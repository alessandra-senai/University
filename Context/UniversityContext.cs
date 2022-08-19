using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Context
{
    public class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PeriodNote> PeriodNotes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                        .HasOne(r => r.CourseAway)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                       .HasOne(r => r.CourseTeam)
                       .WithMany()
                       .OnDelete(DeleteBehavior.Restrict);







            modelBuilder.Entity<Student>().HasData(Seeds.StudentSeed.Seed);
        }
    }
}
