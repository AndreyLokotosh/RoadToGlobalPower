namespace RaodtoGlobalPower.Domain.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateHired { get; set; }

    // Навигационное свойство - у сотрудника много аттестаций
    public List<Attestation> Attestations { get; set; } = new();
}