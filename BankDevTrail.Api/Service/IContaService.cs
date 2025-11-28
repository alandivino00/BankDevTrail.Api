using BankDevTrail.Api.Dto;

namespace BankDevTrail.Api.Service
{
    public interface IContaService
    {
        Task<ContaViewModel> CreateContaAsync(ContaInputModel input);
        Task<ContaViewModel?> GetContaAsync(string numero);
        Task<ContaViewModel?> DepositoAsync(string numero, decimal valor);
        Task<ContaViewModel?> SaqueAsync(string numero, decimal valor);
    }
}
