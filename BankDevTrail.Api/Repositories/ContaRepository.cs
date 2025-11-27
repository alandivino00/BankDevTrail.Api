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

        
    }
}
