using BankDevTrail.Api.Models;
using System.Threading.Tasks;

namespace BankDevTrail.Api.Repositories
{
    public interface IContaRepository
    {
        Task AddAsync(Conta conta);
        Task<Conta?> GetByNumeroAsync(string numero, bool asNoTracking = true);
        Task<Conta?> CreateDepositTransactionAsync(string numero, decimal valor);
        Task<Conta?> CreateWithdrawTransactionAsync(string numero, decimal valor);
        Task<(Conta Origem, Conta Destino)?> CreateTransferTransactionAsync(string numeroOrigem, string numeroDestino, decimal valor);
    }
}
