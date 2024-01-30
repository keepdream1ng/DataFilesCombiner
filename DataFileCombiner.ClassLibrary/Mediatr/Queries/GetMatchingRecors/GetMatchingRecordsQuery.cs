using DataFileCombiner.ClassLibrary.Models;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Queries;
public record GetMatchingRecordsQuery(FilePathsPerExtension MatchingFilesPaths) : IRequest<Result<ICollection<Record>>>;
