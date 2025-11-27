using BankDevTrail.Api.Data;
using BankDevTrail.Api.Dto;
using BankDevTrail.Api.Models;
using BankDevTrail.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankDevTrail.Api.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController(IClienteService service) : ControllerBase
    {
        private readonly IClienteService _service = service;

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] ClienteInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vm = await _service.CreateClienteAsync(input);
                return CreatedAtAction(nameof(GetCliente), new { id = vm.Id }, vm);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
            
        }

        // GET: api/clientes/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var vm = await _service.GetClienteAsync(id);
            if (vm == null)
                return NotFound();

            return Ok(vm);
        }
    }
}
