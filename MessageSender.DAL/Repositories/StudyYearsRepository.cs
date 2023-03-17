using MessageSender.DAL.Context;
using MessageSender.DAL.Models;
using MessageSender.DAL.Repositories.Base;

namespace MessageSender.DAL.Repositories
{
    public class StudyYearsRepository : Repository<StudyYear>
    {
        public StudyYearsRepository(ApplicationDbContext db) : base(db) { }
    }
}
