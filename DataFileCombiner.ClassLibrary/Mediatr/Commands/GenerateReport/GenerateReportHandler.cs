using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Mediatr.Queries;
using DataFileCombiner.ClassLibrary.Models;
using DataFileCombiner.ClassLibrary.Utility;
using DataFilesCombiner.ClassLibrary.ExtensionMethods;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataFileCombiner.ClassLibrary.Mediatr.Commands;
public class GenerateReportHandler(
	IMediator mediator,
	IMatchingDataService dataService,
	IConfiguration configuration
	) : IRequestHandler<GenerateReportCommand, Result<FilePath>>
{
	public async Task<Result<FilePath>> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
	{
		FilePathsPerExtension mathedFilePaths = dataService.GetMatchedFilesPaths();
		if (mathedFilePaths.CsvFilePaths.Length < 1
			|| mathedFilePaths.XmlFilePaths.Length < 1) return Result<FilePath>.Failure();

		List<User> users = new();
		foreach (FilePath path in mathedFilePaths.CsvFilePaths)
		{
			var result = await mediator.Send(new DeserializeUsersFromCsvQuery(path), cancellationToken);
			if (result.IsSuccess) users.AddRange(result.Value!);
		}


		List<Card> cards = new();
		foreach (FilePath path in mathedFilePaths.XmlFilePaths)
		{
			var result = await mediator.Send(new DeserializeCardsFromXmlQuery(path), cancellationToken);
			if (result.IsSuccess) cards.AddRange(result.Value!.CardList);
		}

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
		if (!combinedRecords.Any()) return Result<FilePath>.Failure();

		FilePath reportPath = new( Path.Combine(configuration.GetOutputDirectoryExistingPath(),
			"Report" +	DateTime.Now.ToString("yyyy-dd-MM_HH-mm-ss") + ".json"));
		RecordsContainer records = new() { Records = combinedRecords.ToList() };
		bool success = JsonFileWriter.Write<RecordsContainer>(reportPath, records);
		return success? Result<FilePath>.Success(reportPath) : Result<FilePath>.Failure();
	}
}
