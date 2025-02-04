using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaodtoGlobalPower.Infrastructure.Data;
using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttestationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AttestationController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Получить все аттестации
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestations()
    {
        return await _context.Attestations.ToListAsync();
    }

    // Получить аттестации конкретного сотрудника по его ID
    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestationsByEmployee(int employeeId)
    {
        var attestations = await _context.Attestations
            .Where(a => a.EmployeeId == employeeId)
            .ToListAsync();

        if (!attestations.Any())
        {
            return NotFound("Аттестации не найдены для данного сотрудника.");
        }

        return Ok(attestations);
    }

    // Добавить аттестацию для сотрудника с использованием DTO
    [HttpPost]
    public async Task<ActionResult<Attestation>> AddAttestation([FromBody] AttestationDto attestationDto)
    {
        if (attestationDto == null)
        {
            return BadRequest("Неверные данные.");
        }

        // Преобразуем DTO в модель аттестации
        var attestation = new Attestation
        {
            EmployeeId = attestationDto.EmployeeId,
            Date = attestationDto.Date,
            Name = attestationDto.Name
        };

        // Добавление аттестации в базу данных
        _context.Attestations.Add(attestation);
        await _context.SaveChangesAsync();

        // Возвращаем успешный ответ с созданной аттестацией
        return CreatedAtAction(nameof(GetAttestationsByEmployee), new { employeeId = attestation.EmployeeId }, attestation);
    }
}
