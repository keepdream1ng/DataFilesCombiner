using DataFileCombiner.ClassLibrary.Models;
using DataFileCombiner.ClassLibrary.Utility;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DataFileCombiner.ClassLibrary.Mediatr.Queries;
public class DeserializeUsersFromCsvHandler(
	IConfiguration configuration
	) : IRequestHandler<DeserializeUsersFromCsvQuery, Result<IEnumerable<User>>>
{
	public async Task<Result<IEnumerable<User>>> Handle(DeserializeUsersFromCsvQuery request, CancellationToken cancellationToken)
	{
		return await Task.Run(() =>
			CsvDeserializer.ProcessAllRecords<User>(request.PathRecord.Path, configuration["CsvDelimiter"]!)
		);
	}
}
