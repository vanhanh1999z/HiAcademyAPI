using Microsoft.EntityFrameworkCore;

namespace HiAcademyAPI.Models
{
    public partial class HIACADEMYContext : DbContext
    {
        public HIACADEMYContext()
        {
        }

        public HIACADEMYContext(DbContextOptions<HIACADEMYContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppCourse> AppCourses { get; set; } = null!;
        public virtual DbSet<AppCourseforuser> AppCourseforusers { get; set; } = null!;
        public virtual DbSet<AppCourseinfo> AppCourseinfos { get; set; } = null!;
        public virtual DbSet<AppLession> AppLessions { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-481MNPS;Database=HIACADEMY;Trusted_Connection=True;", x => x.UseHierarchyId());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppCourse>(entity =>
            {
                entity.ToTable("APP_COURSE");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<AppCourseforuser>(entity =>
            {
                entity.HasKey(e => new { e.Iduser, e.Idcourse })
                    .HasName("PK_USER");

                entity.ToTable("APP_COURSEFORUSER");

                entity.Property(e => e.Idcourse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDCOURSE");

                entity.Property(e => e.Iduser)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDUSER");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.IdcourseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idcourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__APP_COURS__IDCOU__2D27B809");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__APP_COURS__STATU__2C3393D0");
            });

            modelBuilder.Entity<AppCourseinfo>(entity =>
            {
                entity.HasKey(e => new { e.Idcourse, e.Idlession })
                    .HasName("PK_SOURCE");

                entity.ToTable("APP_COURSEINFO");

                entity.Property(e => e.Idcourse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDCOURSE");

                entity.Property(e => e.Idlession)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IDLESSION");

                entity.HasOne(d => d.IdcourseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idcourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__APP_COURS__IDCOU__29572725");

                entity.HasOne(d => d.IdlessionNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idlession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__APP_COURS__IDLES__2A4B4B5E");
            });

            modelBuilder.Entity<AppLession>(entity =>
            {
                entity.ToTable("APP_LESSION");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Image)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.Sound)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SOUND");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("APP_USER");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
