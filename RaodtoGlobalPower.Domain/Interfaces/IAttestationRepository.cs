using RaodtoGlobalPower.Domain.Models;

namespace RaodtoGlobalPower.Domain.Interfaces;

public interface IAttestationRepository
{
    Task<IEnumerable<Attestation>> GetAllAsync();
    Task<IEnumerable<Attestation>> GetByEmployeeIdAsync(int employeeId);
    Task<int> AddAsync(Attestation attestation);
}