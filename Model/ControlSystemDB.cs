using System.Data.Common;
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
        }
        );
    }
}