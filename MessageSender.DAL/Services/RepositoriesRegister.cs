using MessageSender.DAL.Interfaces;
using MessageSender.DAL.Models;
using MessageSender.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MessageSender.DAL.Services
{
    public static class RepositoriesRegister
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddScoped<IRepository<Event>, EventsRepository>()
            .AddScoped<IRepository<Professor>, ProfessorsRepository>()
            .AddScoped<IRepository<Student>, StudentsRepository>()
            .AddScoped<IRepository<StudyYear>, StudyYearsRepository>();
    }
}
