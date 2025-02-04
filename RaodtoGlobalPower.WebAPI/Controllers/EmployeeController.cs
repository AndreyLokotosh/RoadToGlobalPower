using Microsoft.AspNetCore.Mvc;
using RaodtoGlobalPower.Domain.Interfaces;
using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Контроллер для работы с <see cref="Employee"/>
    /// </summary>
    /// <param name="employeeRepository"></param>
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Получить всех сотрудников
    /// </summary>
    /// <returns>Список всех сотрудников</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        return Ok(employees);
    }

    /// <summary>
    /// Получить сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Сотрудник</returns>
    /// <response code="200">Возвращает сотрудника</response>
    /// <response code="404">Сотрудник не найден</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        // Ручное преобразование из Employee в EmployeeDto
        var employeeDto = new EmployeeDto
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Position = employee.Position,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            DateHired = employee.DateHired,
            Attestations = employee.Attestations.Select(a => new AttestationDto
            {
                Name = a.Name,
                Date = a.Date
            }).ToList()
        };

        return Ok(employeeDto);
     
    }


    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        await _employeeRepository.AddEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }
        await _employeeRepository.UpdateEmployeeAsync(employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _employeeRepository.DeleteEmployeeAsync(id);
        return NoContent();
    }
}