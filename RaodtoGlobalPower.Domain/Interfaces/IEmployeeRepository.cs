using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
     
    Task<Employee> GetEmployeeByIdAsync(int id);
    
    Task<int> AddEmployeeAsync(Employee employee);
    
    Task UpdateEmployeeAsync(Employee employee);
    
    Task DeleteEmployeeAsync(int id);
    
    Task<IEnumerable<Employee>> GetFilteredEmployeesAsync(
        Position? position, 
        decimal? minSalary, 
        decimal? maxSalary, 
        DateOnly? hiredAfter, 
        DateOnly? hiredBefore);

}