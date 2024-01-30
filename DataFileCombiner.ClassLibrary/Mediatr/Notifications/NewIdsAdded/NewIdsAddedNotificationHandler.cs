using DataFileCombiner.ClassLibrary.Interfaces;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public class NewIdsAddedNotificationHandler(
	IMatchingDataService dataService
	) : INotificationHandler<NewIdsAddedNotification>
{
	public async Task Handle(NewIdsAddedNotification notification, CancellationToken cancellationToken)
	{
		await dataService.CheckMatchesAsync();
	}
}
