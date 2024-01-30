using DataFileCombiner.ClassLibrary.Models;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Queries;
public record DeserializeCardsFromXmlQuery(FilePath PathRecord) : IRequest<Result<Cards>>;
