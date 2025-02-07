using Microsoft.EntityFrameworkCore;
using RaodtoGlobalPower.Domain.Interfaces;
using RaodtoGlobalPower.Domain.Models;
using RaodtoGlobalPower.Infrastructure.Data;

namespace RaodtoGlobalPower.Infrastructure.Repositories;

public class AttestationRepository : IAttestationRepository
{
    private readonly ApplicationDbContext _context;

    public AttestationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Attestation>> GetAllAsync()
    {
        return await _context.Attestations.ToListAsync();
    }

    public async Task<IEnumerable<Attestation>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.Attestations
            .Where(a => a.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task<int> AddAsync(Attestation attestation)
    {
        _context.Attestations.Add(attestation);
        await _context.SaveChangesAsync();
        return attestation.Id;
    }
}