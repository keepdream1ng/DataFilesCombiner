using DataFileCombiner.ClassLibrary.Models;
using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public record ReadyXmlNotification(FilePath PathRecord) : INotification;
