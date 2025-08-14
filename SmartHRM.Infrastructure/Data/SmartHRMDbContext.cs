using Microsoft.EntityFrameworkCore;
using SmartHRM.Core.Entities;

namespace SmartHRM.Infrastructure.Data;

public class SmartHRMDbContext : DbContext
{
    public SmartHRMDbContext(DbContextOptions<SmartHRMDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Organization> Organizations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User mapping
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("tblUsers");
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.UserName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.Property(u => u.IsActive).HasDefaultValue(true);

            entity.HasOne(u => u.Role)
                  .WithMany()
                  .HasForeignKey(u => u.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Role mapping
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("tblRoles");
            entity.HasKey(r => r.RoleId);
            entity.Property(r => r.RoleName).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Description).HasMaxLength(255);
            entity.Property(r => r.IsActive).HasDefaultValue(true);
        });

        // Organization mapping
        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable("tblOrganizations");  // Must match SQL table name
            entity.HasKey(o => o.OrganizationId);
            entity.Property(o => o.OrganizationName).IsRequired().HasMaxLength(255);
            entity.Property(o => o.OrganizationCode).IsRequired().HasMaxLength(50);
            entity.Property(o => o.IsActive).HasDefaultValue(true);
            entity.Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()");
        });

    }
}
