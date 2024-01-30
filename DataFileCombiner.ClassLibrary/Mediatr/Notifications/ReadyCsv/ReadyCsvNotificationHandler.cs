using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Models;
using DataFileCombiner.ClassLibrary.Mediatr.Queries;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public class ReadyCsvNotificationHandler(
	IMediator mediator,
	IProcessedIdRepository repository
	) : INotificationHandler<ReadyCsvNotification>
{
	public async Task Handle(ReadyCsvNotification notification, CancellationToken cancellationToken)
	{
		Result<IEnumerable<User>> usersResult = await mediator.Send(new DeserializeUsersFromCsvQuery(notification.PathRecord), cancellationToken);
		if (usersResult.IsFailure) return;

		IEnumerable<IdFromCsv> newIds = usersResult.Value!
			.Select(u => new IdFromCsv(u.UserId, notification.PathRecord));

		await repository.AddRangeAsync(newIds);
		await mediator.Publish<NewIdsAddedNotification>(new());
	}
}
