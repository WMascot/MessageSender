using MessageSender.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageSender.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(x => new { x.Id, x.Email });
            modelBuilder.Entity<Professor>()
                .HasKey(x => new { x.Id, x.Email });
            modelBuilder.Entity<Event>()
                .HasKey(x => new { x.Id, x.Name });
            modelBuilder.Entity<StudyYear>()
                .HasKey(x => new { x.Id, x.Year });


            modelBuilder.Entity<Student>()
                .HasOne(x => x.Professor)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.ProfessorId)
                .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Student>()
                .HasOne(x => x.StudyYear)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.StudyYearId)
                .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Student>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.EventId)
                .HasPrincipalKey(x => x.Id);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<StudyYear> StudyYears { get; set; }
    }
}
