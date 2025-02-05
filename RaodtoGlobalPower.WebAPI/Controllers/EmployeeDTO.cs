using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.WebAPI.Controllers;


/// <summary>
/// ДТО сотрудника
/// </summary>
public class EmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateOnly DateHired { get; set; }
    public List<AttestationDto> Attestations { get; set; }
}