using Microsoft.AspNetCore.Mvc;
using RaodtoGlobalPower.Domain.Interfaces;
using RaodtoGlobalPower.Domain.Models;
using RaodtoGlobalPower.Infrastructure.Repositories;

namespace RaodtoGlobalPower.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttestationController : ControllerBase
{
    private readonly IAttestationRepository _attestationRepository;
    public AttestationController(IAttestationRepository attestationRepository)
    {
        _attestationRepository = attestationRepository;
    }
    
/// <summary>
/// Получить все аттестации
/// </summary>
/// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestations() 
        => Ok(await _attestationRepository.GetAllAsync());

/// <summary>
/// Получить аттестации конкретного сотрудника по его ID
/// </summary>
/// <param name="employeeId"></param>
/// <returns></returns>
    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestationsByEmployee(int employeeId)
    {
        var attestations = await _attestationRepository.GetByEmployeeIdAsync(employeeId);

        if (!attestations.Any())
        {
            return NotFound("Аттестации не найдены для данного сотрудника.");
        }

        return Ok(attestations);
    }

/// <summary>
/// Добавить аттестацию сотрудника с использованием ДТО
/// </summary>
/// <param name="attestationDto"></param>
/// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<int>> AddAttestation([FromBody] AttestationDto attestationDto)
    {
        if (attestationDto == null)
        {
            return BadRequest("Неверные данные.");
        }
        
        var attestation = new Attestation
        {
            EmployeeId = attestationDto.EmployeeId,
            Date = attestationDto.Date,
            Name = attestationDto.Name
        };
        
        var createdAttestationId = await _attestationRepository.AddAsync(attestation);
        
        return Ok(createdAttestationId);
    }
}