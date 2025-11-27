using BankDevTrail.Api.Models;

namespace BankDevTrail.Api.Repositories
{
    public interface IClienteRepository
    {
        Task<bool> CpfExistsAsync(string cpf);
        Task AddAsync(Cliente cliente);
        Task<Cliente?> GetByClienteIdAsync(Guid clienteId, bool asNoTracking = true);
    }
}
