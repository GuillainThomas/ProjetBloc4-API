using AnnuaireAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireAPI.Data;

public partial class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext() { }

    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options) { }

    public virtual DbSet<Agency> Agencies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agency>(entity =>
        {
            entity.ToTable("Agency");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FixedPhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobilePhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Agency).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Agency");

            entity.HasOne(d => d.Service).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Service");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
