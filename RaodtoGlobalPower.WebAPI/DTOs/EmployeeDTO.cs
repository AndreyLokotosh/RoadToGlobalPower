using RaodtoGlobalPower.Domain.Models;
using System.Text.Json.Serialization;
namespace RaodtoGlobalPower.WebAPI.Controllers;


/// <summary>
/// ДТО сотрудника
/// </summary>
public class EmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Position { get; set; }
    public decimal Salary { get; set; }
    
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly DateOfBirth { get; set; }
    
    [JsonConverter(typeof(DateOnlyConverter))]  
    public DateOnly DateHired { get; set; }
    public List<AttestationDto> Attestations { get; set; }
}