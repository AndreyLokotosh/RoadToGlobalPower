using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Attestation> Attestations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
            .Property(e => e.Position)
            .HasConversion<string>()  
            .HasColumnType("VARCHAR(50)");

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime)
        );
        
        modelBuilder.Entity<Employee>()
            .Property(e => e.DateOfBirth)
            .HasConversion(dateOnlyConverter) 
            .HasColumnType("DATE"); 
        
        modelBuilder.Entity<Employee>()
            .Property(e => e.DateHired)
            .HasConversion(dateOnlyConverter) 
            .HasColumnType("DATE");

        modelBuilder.Entity<Attestation>()
            .HasOne(a => a.Employee)
            .WithMany(e => e.Attestations)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}