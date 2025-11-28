using BankDevTrail.Api.Data;
using BankDevTrail.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDevTrail.Api.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly BankContext _context;

        public ContaRepository(BankContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Conta conta)
        {
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();
        }

        public async Task<Conta?> GetByNumeroAsync(string numero, bool asNoTracking = true)
        {
            var query = _context.Contas.AsQueryable();
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(c => c.Numero == numero);
        }
        public async Task<Conta?> CreateDepositTransactionAsync(string numero, decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor de depósito deve ser maior que zero.", nameof(valor));

            // obtém a conta para edição (rastreadA pelo DbContext)
            var conta = await _context.Contas.FirstOrDefaultAsync(c => c.Numero == numero);
            if (conta == null) return null;

            // atualiza saldo
            conta.Saldo += valor;

            // cria a transação de depósito (ContaOrigemId = null, ContaDestinoId = conta.Id)
            var transacao = new Transacao
            {
                Id = Guid.NewGuid(),
                Valor = valor,
                DataHora = DateTime.UtcNow,
                ContaOrigemId = null,
                ContaDestinoId = conta.Id
                // preencha outros campos de Transacao se existirem (ex.: Tipo)
            };

            // adiciona transação e persiste tudo em um único SaveChangesAsync
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();

            return conta;
        }

        public async Task<Conta?> CreateWithdrawTransactionAsync(string numero, decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor de saque deve ser maior que zero.", nameof(valor));

            // obtém a conta rastreada pelo DbContext
            var conta = await _context.Contas.FirstOrDefaultAsync(c => c.Numero == numero);
            if (conta == null) return null;

            if (conta.Saldo < valor)
                throw new InvalidOperationException("Saldo insuficiente para realizar o saque.");

            conta.Saldo -= valor;

            var transacao = new Transacao
            {
                Id = Guid.NewGuid(),
                Valor = valor,
                DataHora = DateTime.UtcNow,
                ContaOrigemId = conta.Id,
                ContaDestinoId = null
            };

            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();

            return conta;
        }

    }
}
