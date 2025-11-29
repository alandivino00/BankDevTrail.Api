using BankDevTrail.Api.Dto;

namespace BankDevTrail.Api.Service
{
    public interface IContaService
    {
        Task<ContaViewModel> CreateContaAsync(ContaInputModel input);
        Task<ContaViewModel?> GetContaAsync(string numero);
        Task<ContaViewModel?> DepositoAsync(string numero, decimal valor);
        Task<ContaViewModel?> SaqueAsync(string numero, decimal valor);
        Task<TransferResultViewModel?> TransferirAsync(string numeroOrigem, string numeroDestino, decimal valor);
        // Novo: retorna extrato (lista de transações) de uma conta específica
        Task<List<TransacaoViewModel>?> GetExtratoAsync(string numero);
    }
}
