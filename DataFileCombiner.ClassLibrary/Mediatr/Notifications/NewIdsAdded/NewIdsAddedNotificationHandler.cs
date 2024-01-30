using DataFileCombiner.ClassLibrary.Interfaces;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public class NewIdsAddedNotificationHandler(
	IMatchingDataService dataService
	) : INotificationHandler<NewIdsAddedNotification>
{
	public Task Handle(NewIdsAddedNotification notification, CancellationToken cancellationToken)
	{
		dataService.CheckMatches();
		return Task.CompletedTask;
	}
}
