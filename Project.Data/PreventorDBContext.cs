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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student", "public");

                entity.HasKey(x => x.StudentId);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Surname)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}