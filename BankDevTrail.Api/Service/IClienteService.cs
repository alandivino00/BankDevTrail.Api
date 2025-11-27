using BankDevTrail.Api.Dto;

namespace BankDevTrail.Api.Service
{
    public interface IClienteService
    {
        Task<ClienteViewModel> CreateClienteAsync(ClienteInputModel input);
        Task<ClienteViewModel?> GetClienteAsync(Guid ClienteId);
    }
}
