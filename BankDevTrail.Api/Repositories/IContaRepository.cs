using BankDevTrail.Api.Models;

namespace BankDevTrail.Api.Repositories
{
    public interface IContaRepository
    {              
        Task AddAsync(Conta conta);
        Task<Conta?> GetByNumeroAsync(string numero, bool asNoTracking = true);
    }
}
