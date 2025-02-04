namespace RaodtoGlobalPower.Domain.Models;

public class Attestation
{
    public int Id { get; set; }
    public string Name { get; set; } // Например, название аттестации
    public DateTime Date { get; set; } // Дата аттестации

    // Внешний ключ к Employee
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } // Навигационное свойство
}