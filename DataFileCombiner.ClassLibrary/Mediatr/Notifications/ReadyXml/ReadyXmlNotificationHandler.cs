using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public class ReadyXmlNotificationHandler : INotificationHandler<ReadyXmlNotification>
{
	public Task Handle(ReadyXmlNotification notification, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
