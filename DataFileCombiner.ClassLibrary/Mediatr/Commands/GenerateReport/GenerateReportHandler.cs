using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Mediatr.Queries;
using DataFileCombiner.ClassLibrary.Models;
using DataFileCombiner.ClassLibrary.Utility;
using DataFilesCombiner.ClassLibrary.ExtensionMethods;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataFileCombiner.ClassLibrary.Mediatr.Commands;
public class GenerateReportHandler(
	IMediator mediator,
	IMatchingDataService dataService,
	IConfiguration configuration
	) : IRequestHandler<GenerateReportCommand, Result<FilePath>>
{
	public async Task<Result<FilePath>> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
	{
		FilePathsPerExtension mathedFilePaths = dataService.GetMatchedFilesPaths();
		if (mathedFilePaths.CsvFilePaths.Length < 1
			|| mathedFilePaths.XmlFilePaths.Length < 1) return Result<FilePath>.Failure();

		Result<ICollection<Record>> combinedRecordsResult = await mediator.Send(
			new GetMatchingRecordsQuery(mathedFilePaths), cancellationToken);
		if (combinedRecordsResult.IsFailure) return Result<FilePath>.Failure();

		FilePath reportPath = new( Path.Combine(configuration.GetOutputDirectoryExistingPath(),
			"Report" +	DateTime.Now.ToString("yyyy-dd-MM_HH-mm-ss") + ".json"));
		RecordsContainer records = new() { Records = combinedRecordsResult.Value!.ToList() };
		bool success = JsonFileWriter.Write<RecordsContainer>(reportPath, records);
		return success? Result<FilePath>.Success(reportPath) : Result<FilePath>.Failure();
	}
}
