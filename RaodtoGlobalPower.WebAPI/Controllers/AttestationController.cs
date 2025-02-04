using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestations()
    {
        var attestations = await _attestationRepository.GetAllAsync();
        return Ok(attestations);
    }

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

    [HttpPost]
    public async Task<ActionResult<Attestation>> AddAttestation([FromBody] AttestationDto attestationDto)
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

        var createdAttestation = await _attestationRepository.AddAsync(attestation);

        return CreatedAtAction(nameof(GetAttestationsByEmployee), new { employeeId = createdAttestation.EmployeeId }, createdAttestation);
    }
}