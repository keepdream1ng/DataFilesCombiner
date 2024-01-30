using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public class ReadyCsvNotificationHandler : INotificationHandler<ReadyCsvNotification>
{
	public Task Handle(ReadyCsvNotification notification, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
