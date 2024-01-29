using MediatR;

namespace DataFileCombiner.ClassLibrary.Mediatr.Notifications;
public record NewFileNotification(FileSystemEventArgs EventArgs) : INotification;
