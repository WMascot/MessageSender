using MessageSender.DAL.Context;
using MessageSender.DAL.Models;
using MessageSender.DAL.Repositories.Base;

namespace MessageSender.DAL.Repositories
{
    public class EventsRepository : Repository<Event>
    {
        public EventsRepository(ApplicationDbContext db) : base(db) { }
    }
}
