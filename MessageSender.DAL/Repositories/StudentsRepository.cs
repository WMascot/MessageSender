using MessageSender.DAL.Context;
using MessageSender.DAL.Models;
using MessageSender.DAL.Repositories.Base;

namespace MessageSender.DAL.Repositories
{
    public class StudentsRepository : Repository<Student>
    {
        public StudentsRepository(ApplicationDbContext db) : base(db) { }
    }
}
