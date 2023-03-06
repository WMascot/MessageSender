using MessageSender.Console.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessageSender.Console
{
    public class Worker : IHostedService
    {
        private readonly IFileService _fileReaderService;
        private readonly IMailService _mailService;

        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _hostLifeTime;
        private readonly IConfiguration _configuration;
        private int? _exitCode;
        public Worker(IFileService fileReaderService, IMailService mailService, IConfiguration configuration, ILogger<Worker> logger, IHostApplicationLifetime hostLifeTime)
        {
            _fileReaderService = fileReaderService;
            _mailService = mailService;
            _logger = logger;
            _configuration = configuration;
            _hostLifeTime = hostLifeTime;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var messageFilesList = await _fileReaderService.ReadMessageFilesAsync(cancellationToken);
                _logger.LogInformation($"Recieved {messageFilesList.Count} messageFiles");

                if (messageFilesList.Count == 0) return;

                var sendingMessageFilesResultList = await _mailService.SendAsync(messageFilesList, cancellationToken);
                _logger.LogInformation($"Messages sent: {sendingMessageFilesResultList.Count}. Messages total: {messageFilesList.Count}");

                if (sendingMessageFilesResultList.Count == 0) return;

                await _fileReaderService.ChangeMessageFilesStatusAsync(sendingMessageFilesResultList, cancellationToken);
                _logger.LogInformation("Status of sent messages was successfully changed.");
                _exitCode = 0;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("The job has been killed with CTRL+C");
                _exitCode = -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured");
                _exitCode = 1;
            }
            finally
            {
                _hostLifeTime.StopApplication();
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            _logger.LogInformation("Shutting down the service with code {exitCode}", Environment.ExitCode);
            return Task.CompletedTask;
        }
    }
}
