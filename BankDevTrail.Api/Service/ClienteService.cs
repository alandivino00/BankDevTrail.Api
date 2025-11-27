using BankDevTrail.Api.Dto;
using BankDevTrail.Api.Models;
using BankDevTrail.Api.Repositories;

namespace BankDevTrail.Api.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClienteViewModel> CreateClienteAsync(ClienteInputModel input)
        {           
            var clienteExists = await _repo.CpfExistsAsync(input.Cpf);
            if (clienteExists)
                throw new InvalidOperationException("Cpf já cadastrado.");

            var cliente = new Cliente(input.Nome, input.Cpf, input.DataNascimento);

            await _repo.AddAsync(cliente);

            return new ClienteViewModel
            {
                Nome = cliente.Nome,
                DataNascimento = cliente.DataNascimento
            };
        }

        public async Task<ClienteViewModel?> GetClienteAsync(Guid ClienteId)
        {
            var cliente = await _repo.GetByClienteIdAsync(ClienteId);
            if (cliente == null) return null;

            return new ClienteViewModel
            {
                Nome = cliente.Nome,
                DataNascimento = cliente.DataNascimento
            };
        }

    }
    
}
