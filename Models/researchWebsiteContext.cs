using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace researchOnlineWebsite.Models
{
    public partial class researchWebsiteContext : DbContext
    {
        public researchWebsiteContext()
        {
        }

        public researchWebsiteContext(DbContextOptions<researchWebsiteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<Research> Researches { get; set; }
        public virtual DbSet<ResearchContent> ResearchContents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserResearch> UserResearches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.;Database=researchWebsite;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("adminUsers");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AdminUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_adminUsers_roles");
            });

            modelBuilder.Entity<Research>(entity =>
            {
                entity.ToTable("research");

                entity.Property(e => e.ResearchId).HasColumnName("researchId");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .HasColumnName("language");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("date")
                    .HasColumnName("modifiedDate");

                entity.Property(e => e.NumberOfPage).HasColumnName("numberOfPage");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RedirectTo)
                    .HasMaxLength(50)
                    .HasColumnName("redirectTo");

                entity.Property(e => e.Specialize)
                    .HasMaxLength(50)
                    .HasColumnName("specialize");

                entity.Property(e => e.StatId).HasColumnName("statId");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Stat)
                    .WithMany(p => p.Researches)
                    .HasForeignKey(d => d.StatId)
                    .HasConstraintName("FK_research_status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Researches)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_research_users");
            });

            modelBuilder.Entity<ResearchContent>(entity =>
            {
                entity.HasKey(e => e.ResearchId);

                entity.ToTable("researchContent");

                entity.Property(e => e.ResearchId).HasColumnName("researchId");

                entity.Property(e => e.ResearchPdf).HasColumnName("researchPdf");

                entity.Property(e => e.ResearchPdfUrl).HasColumnName("researchPdfUrl");

                entity.Property(e => e.Specialize)
                    .HasMaxLength(50)
                    .HasColumnName("specialize");

                entity.Property(e => e.StatId).HasColumnName("statId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .HasColumnName("statusName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("fullName");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Telephone).HasColumnName("telephone");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_users_roles");
            });

            modelBuilder.Entity<UserResearch>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("userResearch");

                entity.Property(e => e.ResearchId).HasColumnName("researchId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Research)
                    .WithMany()
                    .HasForeignKey(d => d.ResearchId)
                    .HasConstraintName("FK_userResearch_research");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_userResearch_users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
