using DataFileCombiner.ClassLibrary.Models;
using MediatR;
using System.Collections.Concurrent;

namespace DataFileCombiner.ClassLibrary.Mediatr.Queries;
public class GetMatchingRecordsHandler(
	IMediator mediator
	) : IRequestHandler<GetMatchingRecordsQuery, Result<ICollection<Record>>>
{
	public async Task<Result<ICollection<Record>>> Handle(GetMatchingRecordsQuery request, CancellationToken cancellationToken)
	{
		// Next is parallel processing with concurrent collections.
		ConcurrentBag<IEnumerable<User>> usersBag = new();
		ConcurrentBag<IEnumerable<Card>> cardsBag = new();
		List<Task> tasks = new List<Task>();

		foreach (FilePath path in request.MatchingFilesPaths.CsvFilePaths)
		{
			tasks.Add(Task.Run(async () =>
			{
				var result = await mediator.Send(new DeserializeUsersFromCsvQuery(path), cancellationToken);
				if (result.IsSuccess) usersBag.Add(result.Value!);
			}));
		}

		foreach (FilePath path in request.MatchingFilesPaths.XmlFilePaths)
		{
			tasks.Add(Task.Run(async () =>
			{
				var result = await mediator.Send(new DeserializeCardsFromXmlQuery(path), cancellationToken);
				if (result.IsSuccess) cardsBag.Add(result.Value!.CardList);
			}));
		}

		await Task.WhenAll(tasks);
		IEnumerable<User> users = usersBag.SelectMany(user => user);
		IEnumerable<Card> cards = cardsBag.SelectMany(card => card);

		IEnumerable<Record> combinedRecords = from card in cards
											  join user in users on card.UserId equals user.UserId
											  select new Record()
											  {
												  UserId = user.UserId,
												  FirstName = user.Name,
												  LastName = user.SecondName,
												  Phone = user.Number,
												  Pan = card.Pan,
												  ExpDate = card.ExpDate
											  };
		if (!combinedRecords.Any()) return Result<ICollection<Record>>.Failure();
		return Result<ICollection<Record>>.Success(combinedRecords.ToList());
	}
}
