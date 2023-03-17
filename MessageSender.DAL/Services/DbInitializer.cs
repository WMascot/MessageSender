using MessageSender.DAL.Context;
using MessageSender.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MessageSender.DAL.Services
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DbInitializer> _logger;
        public DbInitializer(ApplicationDbContext db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initializing DB...");

            _logger.LogInformation("DB migration...");
            await _db.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("DB migrated after {0} ms", timer.ElapsedMilliseconds);

            if (!await _db.Professors.AnyAsync(cancellationToken).ConfigureAwait(false)) await InitializeProfessors(cancellationToken);
            if (!await _db.Events.AnyAsync(cancellationToken).ConfigureAwait(false)) await InitializeEvents(cancellationToken);
            if (!await _db.StudyYears.AnyAsync(cancellationToken).ConfigureAwait(false)) await InitializeStudyYears(cancellationToken);

            timer.Stop();
            _logger.LogInformation("DB Initialized after {0} ms", timer.ElapsedMilliseconds);
        }

        private Professor[] _Professors;
        private async Task InitializeProfessors(CancellationToken cancellationToken)
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initializing Professors...");

            _Professors = new Professor[]
            {
                new Professor
                {
                    FirstName = "Данила",
                    LastName = "Ефимов",
                    MiddleName = "Алексеевич",
                    Email = "osg-post@mail.ru",
                    ExtraEmail = "danila33366@gmail.com"
                }
            };

            await _db.Professors.AddRangeAsync(_Professors, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            timer.Stop();
            _logger.LogInformation("Professors initialized after {0} ms", timer.ElapsedMilliseconds);
        }

        private Event[] _Events;
        private async Task InitializeEvents(CancellationToken cancellationToken)
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initializing Events...");

            _Events = new Event[]
            {
                new Event
                {
                    Name = "НИР"
                },
                new Event
                {
                    Name = "ВКР"
                }
            };

            await _db.Events.AddRangeAsync(_Events, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            timer.Stop();
            _logger.LogInformation("Events initialized after {0} ms", timer.ElapsedMilliseconds);
        }

        private StudyYear[] _StudyYears;
        private async Task InitializeStudyYears(CancellationToken cancellationToken)
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Initializing StudyYears...");

            _StudyYears = new StudyYear[]
            {
                new StudyYear
                {
                    Year = 3
                },
                new StudyYear
                {
                    Year = 4
                }
            };

            await _db.StudyYears.AddRangeAsync(_StudyYears, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            timer.Stop();
            _logger.LogInformation("StudyYears initialized after {0} ms", timer.ElapsedMilliseconds);
        }
    }
}
