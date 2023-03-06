using MessageSender.Console.Interfaces;
using MessageSender.Console.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MessageSender.Console.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly string _filePath;
        public FileService(ILogger<FileService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _filePath = Path.Combine(Environment.CurrentDirectory, configuration["FilePath"] ?? "");
            if (!Directory.Exists(_filePath)) Directory.CreateDirectory(_filePath);
        }
        public async Task<List<MessageFile>> ReadMessageFilesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Started preparing messageFiles.");
            List<MessageFile> messageFilesList = new();

            _logger.LogInformation($"Looking for messages to send in the directory {_filePath}.");
            var messageFiles = Directory.GetFiles(_filePath);

            _logger.LogInformation($"Found {messageFiles.Length} message file[s] at this directory.");
            foreach (var message in messageFiles)
            {
                _logger.LogInformation($"Analyzing message {message}");
                var fileMessageLines = await File.ReadAllLinesAsync(message, cancellationToken);
                fileMessageLines = fileMessageLines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                if (fileMessageLines.Length < 6)
                {
                    _logger.LogWarning($"Message skipped due to having less then 6 lines.");
                    continue;
                }
                _logger.LogInformation($"Found {fileMessageLines.Length} lines in message.");

                MessageFile messageFile;
                try
                {
                    messageFile = new MessageFile
                    {
                        Directory = message,
                        SendingDate = DateOnly.Parse(fileMessageLines[0]),
                        MessageSubject = fileMessageLines[1],
                        MessageBody = String.Join("\n", fileMessageLines[2..(fileMessageLines.Length - 3)]),
                        FullName = fileMessageLines[^3],
                        Destination = fileMessageLines[^2],
                        MessageStatus = (MessageStatus)Enum.Parse(typeof(MessageStatus), fileMessageLines[^1])
                    };

                    if (messageFile.MessageStatus == MessageStatus.READY && messageFile.SendingDate == DateOnly.FromDateTime(DateTime.Now)) messageFilesList.Add(messageFile);
                    else if (messageFile.MessageStatus == MessageStatus.SENT) _logger.LogWarning("The message already sent.");
                    else _logger.LogWarning("The message sending date doesn't equal today's date.");

                    _logger.LogInformation("Message analyzed");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occured during creating messageFile instance. Directory: {message}");
                    _logger.LogWarning("Message skipped.");
                    continue;
                }
            }

            _logger.LogInformation("Finished preparing messageFiles.");
            return messageFilesList;
        }
        public async Task ChangeMessageFilesStatusAsync(List<MessageFile> messageFiles, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Started changing the status of sent messages.");
            foreach (var messageFile in messageFiles)
            {
                string messageFilePath = messageFile.Directory;
                string date = messageFile.SendingDate.ToString("yyyy-MM-dd");
                List<string> bodyStrings = messageFile.MessageBody.Split("\n").ToList();
                string messageStatus = messageFile.MessageStatus.ToString();

                List<string> messageFileLines = new();
                messageFileLines.Add(date);
                messageFileLines.Add(messageFile.MessageSubject);
                messageFileLines.AddRange(bodyStrings);
                messageFileLines.Add(messageFile.FullName);
                messageFileLines.Add(messageFile.Destination);
                messageFileLines.Add(messageStatus);

                var result = messageFileLines.ToArray();
                await File.WriteAllLinesAsync(messageFilePath, result, cancellationToken);
            }

            _logger.LogInformation("Finished changing the status of sent messages.");
        }
    }
}
