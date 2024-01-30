using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public record NewFileNotification(string FilePath) : INotification;
