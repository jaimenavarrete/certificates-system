using CertificatesSystem.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Models.Data.Database
{
    public partial class CertificatesSystemContext : DbContext
    {
        public CertificatesSystemContext()
        {
        }

        public CertificatesSystemContext(DbContextOptions<CertificatesSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentGrade> StudentGrades { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                
                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Name)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Nie);

                entity.Property(e => e.Nie)
                    .ValueGeneratedNever()
                    .HasColumnName("NIE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudentGrade>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Nie)
                    .HasColumnName("NIE");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grades");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.Nie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sections");
            });
        }
    }
}
