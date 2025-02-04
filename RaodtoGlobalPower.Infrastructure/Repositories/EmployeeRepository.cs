// Infrastructure/Repositories/EmployeeRepository.cs

using Microsoft.EntityFrameworkCore;
using RaodtoGlobalPower.Domain.Interfaces;
using RaodtoGlobalPower.Domain.Models;
using RaodtoGlobalPower.Infrastructure.Data;

namespace RaodtoGlobalPower.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        var a = await _context.Employees.ToListAsync();
        return await _context.Employees.ToListAsync();
    }
    
    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        var a = await _context.Employees
            .Include(x => x.Attestations)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        return a;
    }

    public async Task<int> AddEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee.Id;
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}