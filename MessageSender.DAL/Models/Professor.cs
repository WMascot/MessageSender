using MessageSender.DAL.Models.Base;

namespace MessageSender.DAL.Models
{
    public class Professor : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string ExtraEmail { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Professor()
        {
            Students = Students ?? new List<Student>();
        }
    }
}
