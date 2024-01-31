using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Mediatr.Notifications;
using DataFileCombiner.ClassLibrary.Models;
using DataFilesCombiner.ClassLibrary.ExtensionMethods;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataFileCombiner.ClassLibrary.Mediatr.Commands;
public class ResetAndRereadFilesHandler(
	IMediator mediator,
	IProcessedIdRepository repository,
	IConfiguration configuration
	) : IRequestHandler<ResetAndRereadFilesCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(ResetAndRereadFilesCommand request, CancellationToken cancellationToken)
	{
		try
		{
			await repository.Reset();
			// Refresh count in the ui.
			await mediator.Publish<NewIdsAddedNotification>(new());

			// Process all files in the input folder.
			string[] filePaths = Directory.GetFiles(configuration.GetInputDirectoryExistingPath());
			foreach ( string filePath in filePaths )
			{
				await mediator.Publish<NewFileNotification>(new(filePath));
			}
			
			return Result<bool>.Success(true);
		}
		catch
		{
			return Result<bool>.Failure();
		}
	}
}
