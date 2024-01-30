using DataFileCombiner.ClassLibrary.Utility;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
internal class NewFileNotificationHandler : INotificationHandler<NewFileNotification>
{
	public Task Handle(NewFileNotification notification, CancellationToken cancellationToken)
	{
		string filePath = notification.EventArgs.FullPath;

		do
		{
			Thread.Sleep(500);
		} while (!WorkingFolders.HasReadAccess(filePath));

		Console.WriteLine($"File {notification.EventArgs.ChangeType.ToString()}: {filePath}");
		return Task.CompletedTask;
	}
}
