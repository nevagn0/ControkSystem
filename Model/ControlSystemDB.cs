using System.Data.Common;
using ControkSystem.Model;
using Microsoft.EntityFrameworkCore;
using ControkSystem.Models;
namespace ControkSystem.DBContex;

public class ControlSystemDbContext : DbContext
{
    public ControlSystemDbContext(DbContextOptions<ControlSystemDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            entity.Property(e => e.Login)
                .HasMaxLength(250)
                .IsRequired();

            entity.Property(e => e.HashPassword)
                .IsRequired();

            entity.Property(e => e.Type)
                .IsRequired();
        });
        modelBuilder.Entity<Projects>(entity =>
        {
            entity.ToTable("Projects");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsRequired();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .IsRequired();
            entity.Property(e => e.Progres)
                .HasMaxLength(250)
                .IsRequired();
        });
        modelBuilder.Entity<UserProject>(entity =>
        {
            entity.ToTable("UserProject");
            entity.HasKey(e => new { Id_User = e.IdUser, Id_Project = e.IdProject});
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(e => e.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Projects)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(e => e.IdProject)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Defects>(entity =>
        {
            entity.ToTable("Defects");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Project)
                .WithMany(p => p.Defects)
                .HasForeignKey(e => e.IdProject)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsRequired();
            entity.Property(e => e.Comm)
                .HasMaxLength(250)
                .IsRequired();
            entity.Property(e => e.DeadLine)
                .HasMaxLength(250)
                .IsRequired();
            entity.Property(e => e.Pictures)
                .IsRequired();
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .IsRequired();
            entity.Property(e => e.Priority)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .IsRequired();
        });
    }
}