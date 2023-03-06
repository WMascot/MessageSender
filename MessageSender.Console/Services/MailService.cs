using MailKit.Net.Smtp;
using MailKit.Security;
using MessageSender.Console.Interfaces;
using MessageSender.Console.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MessageSender.Console.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<MailSettings> settings, ILogger<MailService> logger)
        {
            _mailSettings = settings.Value;
            _logger = logger;
        }
        public async Task<List<MessageFile>> SendAsync(List<MessageFile> messageFiles, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Started sending messages.");

            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From));
            mail.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From);
            _logger.LogInformation("Created Mailbox depending on your data.");

            _logger.LogInformation("Sending messages...");
            foreach (var messageFile in messageFiles)
            {
                try
                {
                    mail.To.Add(MailboxAddress.Parse(messageFile.Destination));

                    var body = new BodyBuilder();
                    mail.Subject = messageFile.MessageSubject;
                    body.TextBody = messageFile.MessageBody;
                    mail.Body = body.ToMessageBody();

                    using var smtp = new SmtpClient();
                    if (_mailSettings.UseSSL) await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.SmtpPort, SecureSocketOptions.SslOnConnect, cancellationToken);
                    if (_mailSettings.UseStartTls) await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
                    await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.EmailPassword, cancellationToken);
                    await smtp.SendAsync(mail, cancellationToken);
                    await smtp.DisconnectAsync(true, cancellationToken);

                    mail.To.Remove(MailboxAddress.Parse(messageFile.Destination));

                    messageFile.MessageStatus = MessageStatus.SENT;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occured during sending message {messageFile.Directory}.");
                    messageFiles.Remove(messageFile);
                    continue;
                }
            }

            _logger.LogInformation("Finished sending messages");
            return messageFiles;
        }
    }
}
