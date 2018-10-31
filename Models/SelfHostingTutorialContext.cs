using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataVisualiser.Models
{
    public partial class SelfHostingTutorialContext : DbContext
    {
        public SelfHostingTutorialContext()
        {
        }

        public SelfHostingTutorialContext(DbContextOptions<SelfHostingTutorialContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TPost> TPost { get; set; }
        public virtual DbSet<TUser> TUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:ufgator.database.windows.net,1433;Initial Catalog=SelfHostingTutorial;Persist Security Info=False;User ID=karans;Password=Kaaran*sh2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TPost>(entity =>
            {
                entity.HasKey(e => e.PkPostId);

                entity.ToTable("tPost");

                entity.Property(e => e.PkPostId)
                    .HasColumnName("PK_PostID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkEmailD)
                    .IsRequired()
                    .HasColumnName("FK_EmailD")
                    .HasMaxLength(125);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.PostContent).HasColumnType("text");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.PkEmailId);

                entity.ToTable("tUser");

                entity.Property(e => e.PkEmailId)
                    .HasColumnName("PK_EmailID")
                    .HasMaxLength(125)
                    .ValueGeneratedNever();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(125);
            });
        }
    }
}
