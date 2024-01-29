using DataFileCombiner.ClassLibrary.Utility;
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

    public FileWatcherService(
		IConfiguration configuration
		)
    {
		_pathToWach = WorkingFolders.GetExistingFullPath(configuration["InputDirectoryPath"]);
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

	private void NotifyAboutFile(object sender, FileSystemEventArgs e)
	{
		// Handle the file creation event
		string filePath = e.FullPath;

		do
		{
			Thread.Sleep(500);
		} while (!HasReadAccess(filePath));

		Console.WriteLine($"File {e.ChangeType.ToString()}: {filePath}");
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
