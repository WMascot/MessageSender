using MessageSender.Console;
using MessageSender.Console.Interfaces;
using MessageSender.Console.Models;
using MessageSender.Console.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

class Program
{
    public static async Task<int> Main(string[] args)
    {
        var host = CreateHostBuilder(args);
        await host.RunConsoleAsync();
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
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>()
                        .AddTransient<IFileService, FileService>()
                        .AddTransient<IMailService, MailService>();

                services.Configure<MailSettings>(hostContext.Configuration.GetRequiredSection("MailSettings"));
            });
}