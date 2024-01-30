using DataFileCombiner.ClassLibrary.Models;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public record ReadyCsvNotification(FilePath PathRecord) : INotification;
