using BankDevTrail.Api.Dto;
using BankDevTrail.Api.Models;
using BankDevTrail.Api.Repositories;

namespace BankDevTrail.Api.Service
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IClienteRepository _clienteRepository;

        public ContaService(IContaRepository contaRepository, IClienteRepository clienteRepository)
        {
            _contaRepository = contaRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<ContaViewModel> CreateContaAsync(ContaInputModel input)
        {
            if (input == null)
                throw new InvalidOperationException("Dados da conta inválidos.");

            if (input.ClienteId == Guid.Empty)
                throw new InvalidOperationException("ClienteId inválido.");

            var clienteExists = await _clienteRepository.ClienteExistsAsync(input.ClienteId);
            if (!clienteExists)
                throw new InvalidOperationException("Cliente não encontrado.");

            var conta = new Conta
            {
                Numero = input.Numero,
                Saldo = input.Saldo,
                ClienteId = input.ClienteId,
                Status = input.Status,
                Tipo = input.Tipo
            };

            await _contaRepository.AddAsync(conta);

            var cliente = await _clienteRepository.GetByClienteIdAsync(input.ClienteId, asNoTracking: true);

            return new ContaViewModel
            {
                Numero = conta.Numero,
                Titular = cliente?.Nome ?? string.Empty,
                Saldo = conta.Saldo,
                ClienteId = conta.ClienteId
            };
        }

        public async Task<ContaViewModel?> GetContaAsync(string numero)
        {
            var conta = await _contaRepository.GetByNumeroAsync(numero);
            if (conta == null)
                return null;

            string titular = string.Empty;
            if (conta.ClienteId.HasValue)
            {
                var cliente = await _clienteRepository.GetByClienteIdAsync(conta.ClienteId.Value, asNoTracking: true);
                titular = cliente?.Nome ?? string.Empty;
            }

            return new ContaViewModel
            {
                Numero = conta.Numero,
                Titular = titular,
                Saldo = conta.Saldo,
                ClienteId = conta.ClienteId
            };
        }
    }
}
