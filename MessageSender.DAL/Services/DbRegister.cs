using MessageSender.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageSender.DAL.Services
{
    public static class DbRegister
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<ApplicationDbContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("MySQL");
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            })
            .AddTransient<DbInitializer>()
            .AddRepositories();
    }
}
