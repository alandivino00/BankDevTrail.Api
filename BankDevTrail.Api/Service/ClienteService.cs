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
            // solicita o cliente incluindo as contas (o repositório deve incluir as Contas quando apropriado)
            var cliente = await _repo.GetByClienteIdAsync(ClienteId, asNoTracking: true);
            if (cliente == null) return null;

            return new ClienteViewModel
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                DataNascimento = cliente.DataNascimento,
                Contas = cliente.Contas?
                    .Select(c => new ContaViewModel
                    {
                        Numero = c.Numero,
                        Titular = cliente.Nome,
                        Saldo = c.Saldo,
                        ClienteId = c.ClienteId
                    })
                    .ToList() ?? new List<ContaViewModel>()
            };
        }

    }
    
}
