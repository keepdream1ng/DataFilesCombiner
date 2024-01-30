using DataFileCombiner.ClassLibrary.Models;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Commands;
public class GenerateReportCommand : IRequest<Result<FilePath>>;
