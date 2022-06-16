using CertificatesSystem.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CertificatesSystem.Models.Data.Database
{
    public partial class CertificatesSystemContextSqlServer : DbContext
    {
        public CertificatesSystemContextSqlServer()
        {
        }

        public CertificatesSystemContextSqlServer(DbContextOptions<CertificatesSystemContextSqlServer> options)
            : base(options)
        {
        }

        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grades");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students");
            });
            
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
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
                entity.HasIndex(e => e.Nie, "UQ_Nie_Students")
                    .IsUnique();

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Birthdate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
