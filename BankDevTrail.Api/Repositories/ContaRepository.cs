using BankDevTrail.Api.Data;
using BankDevTrail.Api.Models;
using Microsoft.AspNetCore.Authentication;

namespace BankDevTrail.Api.Repositories
{
    public class ContaRepository : IContaRepository
    {

        private readonly BankContext _context;

        public ContaRepository(BankContext context)
        {
            _context = context;
        }

        //public async Task AddAsync(Conta conta)
        //{
        //    await _context.Contas.AddAsync(conta);
        //    await _context.SaveChangesAsync();
        //}

    }
}
