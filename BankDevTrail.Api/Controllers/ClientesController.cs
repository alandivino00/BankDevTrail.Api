using System;
using System.Threading.Tasks;
using BankDevTrail.Api.Data;
using BankDevTrail.Api.Dto;
using BankDevTrail.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankDevTrail.Api.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly BankContext _context;

        public ClientesController(BankContext context)
        {
            _context = context;
        }        

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] ClienteInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _context.Clientes.AsNoTracking().AnyAsync(c => c.Cpf == input.Cpf);
            if (exists)
            {
                ModelState.AddModelError(nameof(input.Cpf), "CPF já cadastrado.");
                return BadRequest(ModelState);
            }

            var cliente = new Cliente
            {
                Nome = input.Nome,
                Cpf = input.Cpf,
                DataNascimento = input.DataNascimento
            };

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // GET: api/clientes/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var cliente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }
    }
}
