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
    public DateTime DateOfBirth { get; set; }
    public DateTime DateHired { get; set; }
    public List<AttestationDto> Attestations { get; set; }
}