using BankDevTrail.Api.Data;
using BankDevTrail.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDevTrail.Api.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BankContext _context;
        public ClienteRepository(BankContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _context.Clientes.AsNoTracking().AnyAsync(c => c.Cpf == cpf);
        }

        public async Task<Cliente?> GetByClienteIdAsync(Guid clienteId, bool asNoTracking = true)
        {
            var query = _context.Clientes.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.Include(c => c.Contas)
                              .FirstOrDefaultAsync(c => c.Id == clienteId);
        }

    }
}
