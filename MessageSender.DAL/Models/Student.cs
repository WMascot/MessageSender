using MessageSender.DAL.Models.Base;

namespace MessageSender.DAL.Models
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public string? ExtraEmail { get; set; }
        public int ProfessorId { get; set; }
        public int EventId { get; set; }
        public int StudyYearId { get; set; }

        public virtual Professor Professor { get; set; }
        public virtual Event Event { get; set; }
        public virtual StudyYear StudyYear { get; set; }
    }
}
