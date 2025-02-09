using System.Text.Json.Serialization;
using RaodtoGlobalPower.Domain.Models;

public class EmployeeFilterDTO
{
    public Position? Position { get; set; }
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }

    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly? HiredAfter { get; set; }

    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly? HiredBefore { get; set; }
}