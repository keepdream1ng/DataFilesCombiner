using DataFileCombiner.ClassLibrary.Mediatr.Notifications;
using DataFileCombiner.ClassLibrary.Utility;
using DataFilesCombiner.ClassLibrary.ExtensionMethods;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DataFilesCombiner.App.Services;
/// <summary>
/// Service for checking input folder and notifying in case new data files added.
/// </summary>
public class FileWatcherService : IHostedService
{
	private FileSystemWatcher? _watcher;
	private string _pathToWach;
	private readonly IMediator _mediator;

	public FileWatcherService(
		IConfiguration configuration,
		IMediator mediator
		)
    {
		_pathToWach = configuration.GetInputDirectoryExistingPath();
		_mediator = mediator;
	}

    public Task StartAsync(CancellationToken cancellationToken)
	{
		StartFileWatcher();
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		StopFileWatcher();
		return Task.CompletedTask;
	}

	private void StartFileWatcher()
	{
		Console.WriteLine($"Watching {_pathToWach}");
		_watcher = new FileSystemWatcher();
		_watcher.Path = _pathToWach;
		_watcher.Created += NotifyAboutFile;
		_watcher.EnableRaisingEvents = true;
	}

	private void StopFileWatcher()
	{
		if (_watcher is not null)
		{
			// Unsubscribe from events
			_watcher.Created -= NotifyAboutFile;

			_watcher.Dispose();
			_watcher = null;
		}
	}

	private async void NotifyAboutFile(object sender, FileSystemEventArgs e)
	{
		await _mediator.Publish<NewFileNotification>(new(e.FullPath));
	}
}
