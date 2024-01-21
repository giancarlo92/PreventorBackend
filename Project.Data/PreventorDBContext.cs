using Microsoft.EntityFrameworkCore;
using Project.Entities.Model;

namespace Project.Data
{
    public partial class PreventorDBContext : DbContext
    {
        public PreventorDBContext(DbContextOptions<PreventorDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student", "public");

                entity.HasKey(x => x.StudentId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Surname).IsRequired();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course", "public");

                entity.HasKey(x => x.CourseId);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("studentcourse", "public");

                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                entity
                    .HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.StudentId);

                entity
                    .HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentCourses)
                    .HasForeignKey(sc => sc.CourseId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}