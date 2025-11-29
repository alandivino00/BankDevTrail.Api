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

        public async Task<(Conta Origem, Conta Destino)?> CreateTransferTransactionAsync(string numeroOrigem, string numeroDestino, decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor de transferência deve ser maior que zero.", nameof(valor));

            if (string.Equals(numeroOrigem, numeroDestino, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Conta de origem e destino devem ser diferentes.", nameof(numeroDestino));

            // carrega as duas contas rastreadas pelo mesmo DbContext
            var origem = await _context.Contas.FirstOrDefaultAsync(c => c.Numero == numeroOrigem);
            var destino = await _context.Contas.FirstOrDefaultAsync(c => c.Numero == numeroDestino);

            if (origem == null || destino == null)
                return null;

            if (origem.Saldo < valor)
                throw new InvalidOperationException("Saldo insuficiente para realizar a transferência.");

            // aplica mudanças de saldo
            origem.Saldo -= valor;
            destino.Saldo += valor;

            // cria duas transações com tipos distintos: TransferenciaEnviada (débito) e TransferenciaRecebida (crédito)
            var transacaoEnviada = new Transacao
            {
                Id = Guid.NewGuid(),
                Valor = valor,
                DataHora = DateTime.UtcNow,
                ContaOrigemId = origem.Id,
                ContaDestinoId = destino.Id,
                Tipo = TipoTransacao.TransferenciaEnviada
            };

            var transacaoRecebida = new Transacao
            {
                Id = Guid.NewGuid(),
                Valor = valor,
                DataHora = DateTime.UtcNow,
                ContaOrigemId = origem.Id,
                ContaDestinoId = destino.Id,
                Tipo = TipoTransacao.TransferenciaRecebida
            };

            await _context.Transacoes.AddRangeAsync(transacaoEnviada, transacaoRecebida);

            // Single SaveChangesAsync garante atomicidade
            await _context.SaveChangesAsync();

            return (origem, destino);
        }

        // Nova implementação: retorna as transações relacionadas a uma conta (por número)
        public async Task<List<Transacao>?> GetTransacoesByContaNumeroAsync(string numero)
        {
            var conta = await _context.Contas.AsNoTracking().FirstOrDefaultAsync(c => c.Numero == numero);
            if (conta == null) return null;

            var transacoes = await _context.Transacoes
                .Where(t => t.ContaOrigemId == conta.Id || t.ContaDestinoId == conta.Id)
                .OrderByDescending(t => t.DataHora)
                .ToListAsync();

            return transacoes;
        }

    }
}
