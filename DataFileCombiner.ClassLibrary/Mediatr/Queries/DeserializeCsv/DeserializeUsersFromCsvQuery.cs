using DataFileCombiner.ClassLibrary.Models;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Queries;
public record DeserializeUsersFromCsvQuery(FilePath PathRecord) : IRequest<Result<IEnumerable<User>>>;
