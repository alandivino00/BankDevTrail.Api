using BankDevTrail.Api.Models;
using System.Threading.Tasks;

namespace BankDevTrail.Api.Repositories
{
    public interface IContaRepository
    {
        Task AddAsync(Conta conta);
        Task<Conta?> GetByNumeroAsync(string numero, bool asNoTracking = true);
        Task<Conta?> DepositarAsync(string numero, decimal valor);
    }
}
