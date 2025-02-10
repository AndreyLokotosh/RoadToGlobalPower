using RaodtoGlobalPower.Domain.Models;

public class EmployeeFilterDto
{
    public Position? Position { get; set; }
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }

    public DateOnly? HiredAfter { get; set; }

    public DateOnly? HiredBefore { get; set; }
}
