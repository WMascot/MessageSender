using MessageSender.DAL.Models.Base;

namespace MessageSender.DAL.Models
{
    public class Event : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Event()
        {
            Students = Students ?? new List<Student>();
        }
    }
}
