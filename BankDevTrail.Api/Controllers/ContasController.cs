using BankDevTrail.Api.Dto;
using BankDevTrail.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace BankDevTrail.Api.Controllers
{
    [ApiController]
    [Route("api/contas")]
    public class ContasController : ControllerBase
    {
        private readonly IContaService _service;

        public ContasController(IContaService service)
        {
            _service = service;
        }

        // POST: api/contas
        [HttpPost]
        public async Task<ActionResult<ContaViewModel>> CreateConta([FromBody] ContaInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vm = await _service.CreateContaAsync(input);
                return CreatedAtAction(nameof(GetConta), new { numero = vm.Numero }, vm);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        // GET: api/contas/{numero}
        [HttpGet("{numero}")]
        public async Task<ActionResult<ContaViewModel>> GetConta(string numero)
        {
            var vm = await _service.GetContaAsync(numero);
            if (vm == null)
                return NotFound();

            return Ok(vm);
        }

        // PUT: acrescenta saldo na conta especificada
        [HttpPut("{numero}/deposito")]
        public async Task<ActionResult<ContaViewModel>> Deposito(string numero, [FromBody] DepositoInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vm = await _service.DepositoAsync(numero, input.Valor);
            if (vm == null)
                return NotFound();

            return Ok(vm);
        }

        // POST api/contas/{numero}/saque
        [HttpPost("{numero}/saque")]
        public async Task<IActionResult> Saque(string numero, [FromBody] SaqueInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var vm = await _service.SaqueAsync(numero, input.Valor);
                if (vm == null) return NotFound();
                return Ok(vm);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // saldo insuficiente ou regra de negócio
                return BadRequest(ex.Message);
            }
        }

    }
}
