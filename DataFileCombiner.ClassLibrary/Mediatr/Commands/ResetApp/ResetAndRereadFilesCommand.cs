using DataFileCombiner.ClassLibrary.Models;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Commands;
public record ResetAndRereadFilesCommand : IRequest<Result<bool>>;
