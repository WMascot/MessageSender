using MessageSender.Console.Models;

namespace MessageSender.Console.Interfaces
{
    public interface IFileService
    {
        Task<List<MessageFile>> ReadMessageFilesAsync(CancellationToken cancellationToken = default);
        Task ChangeMessageFilesStatusAsync(List<MessageFile> messageFiles, CancellationToken cancellationToken = default);
    }
}
