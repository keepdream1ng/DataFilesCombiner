using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Models;
using DataFileCombiner.ClassLibrary.Mediatr.Queries;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public class ReadyXmlNotificationHandler(
	IMediator mediator,
	IProcessedIdRepository repository
	) : INotificationHandler<ReadyXmlNotification>
{
	public async Task Handle(ReadyXmlNotification notification, CancellationToken cancellationToken)
	{
		Result<Cards> cardResult = await mediator.Send(new DeserializeCardsFromXmlQuery(notification.PathRecord), cancellationToken);
		if (cardResult.IsFailure) return;

		IEnumerable<IdFromXml> newIds = cardResult.Value!.CardList
			.Select(c => new IdFromXml(c.UserId, notification.PathRecord));

		await repository.AddRangeAsync(newIds);
		await mediator.Publish<NewIdsAddedNotification>(new());
	}
}
