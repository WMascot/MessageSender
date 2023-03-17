using MessageSender.DAL.Models.Base;

namespace MessageSender.DAL.Models
{
    public class StudyYear : Entity
    {
        public int Year { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public StudyYear()
        {
            Students = Students ?? new List<Student>();
        }
    }
}
