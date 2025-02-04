using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.Infrastructure.Repositories;

public interface IAttestationRepository
{
    Task<IEnumerable<Attestation>> GetAllAsync();
    Task<IEnumerable<Attestation>> GetByEmployeeIdAsync(int employeeId);
    Task<Attestation> AddAsync(Attestation attestation);
}