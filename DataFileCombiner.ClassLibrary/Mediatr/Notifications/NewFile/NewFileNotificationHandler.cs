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
		} while (!HasReadAccess(filePath));

		Console.WriteLine($"File {notification.EventArgs.ChangeType.ToString()}: {filePath}");
		return Task.CompletedTask;
	}

	private bool HasReadAccess(string filePath)
	{
		try
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				// If the file can be opened for reading, you have read access
				return true;
			}
		}
		catch (Exception)
		{
			return false;
		}
	}
}
