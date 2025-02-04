using Microsoft.EntityFrameworkCore;
using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Attestation> Attestations { get; set; } // 👈 Добавляем аттестации

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настраиваем связь один-ко-многим
        modelBuilder.Entity<Attestation>()
            .HasOne(a => a.Employee)
            .WithMany(e => e.Attestations)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}