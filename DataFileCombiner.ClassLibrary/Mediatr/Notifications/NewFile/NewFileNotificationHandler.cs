using DataFileCombiner.ClassLibrary.Models;
using DataFileCombiner.ClassLibrary.Utility;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
internal class NewFileNotificationHandler(
	IConfiguration configuration,
	IMediator mediator
	) : INotificationHandler<NewFileNotification>
{
	private readonly int _fileCheckTimeout = configuration.GetValue<int>("FileCheckTimeoutMs");
	private int fileCheckAttemptsLeft = configuration.GetValue<int>("FileCheckAttempts");
	public Task Handle(NewFileNotification notification, CancellationToken cancellationToken)
	{
		FilePath filePath = new(notification.FilePath);

		// Just in case of large files being copied on disk.
		do
		{
			// No magic numbers here.
			Thread.Sleep(_fileCheckTimeout);
			if (--fileCheckAttemptsLeft < 0)
			{
				return Task.CompletedTask; 
			}
		} while (!WorkingFolders.HasReadAccess(filePath.Path));

		string extension = Path.GetExtension(filePath.Path).ToLower();
		switch (extension)
		{
			case ".xml":
				mediator.Publish<ReadyXmlNotification>(new(filePath));
				break;

			case ".csv":
				mediator.Publish<ReadyCsvNotification>(new(filePath));
				break;

			default: break;
		}
		return Task.CompletedTask;
	}
}
