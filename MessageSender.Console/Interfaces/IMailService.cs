using MessageSender.Console.Models;

namespace MessageSender.Console.Interfaces
{
    public interface IMailService
    {
        Task<List<MessageFile>> SendAsync(List<MessageFile> messageFiles, CancellationToken cancellationToken = default);
    }
}
