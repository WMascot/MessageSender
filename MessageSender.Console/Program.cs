using MessageSender.Console.Interfaces;
using MessageSender.Console.Models;
using MessageSender.Console.Services;
using MessageSender.DAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

class Program
{
    public static async Task<int> Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var fileService = host.Services.GetRequiredService<IFileService>();
        var mailService = host.Services.GetRequiredService<IMailService>();

        using (var scope = host.Services.CreateScope())
            await scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().ConfigureAwait(false);

        var messageFiles = await fileService.ReadMessageFilesAsync();
        var sendedMessages = await mailService.SendAsync(messageFiles);

        await fileService.ChangeMessageFilesStatusAsync(sendedMessages);
        return Environment.ExitCode;
    }
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            })
            .UseSerilog((hostContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
            })
            .ConfigureAppConfiguration((hostContext, builder) =>
            {
                builder.AddEnvironmentVariables();

                if (hostContext.HostingEnvironment.IsDevelopment())
                    builder.AddUserSecrets<Program>();
            })
            .ConfigureServices(async (hostContext, services) =>
            {
                services.AddDataBase(hostContext.Configuration)
                        .AddTransient<IFileService, FileService>()
                        .AddTransient<IMailService, MailService>();

                services.Configure<MailSettings>(hostContext.Configuration.GetRequiredSection("MailSettings"));
            });
}