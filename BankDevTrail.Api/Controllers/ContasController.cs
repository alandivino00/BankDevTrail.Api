using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankDevTrail.Api.Controllers
{
    [ApiController]
    [Route("api/contas")]
    public class ContasController : ControllerBase
    {
        //private readonly IContaService _service = service;

        // POST: cria nova conta, valida e retorna 201 com Location
        //[HttpPost]
        //public async Task<ActionResult<ContaViewModel>> CreateConta([FromBody] ContaInputModel input)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var vm = await _service.CreateContaAsync(input);
        //        return CreatedAtAction(nameof(GetConta), new { numero = vm.Numero }, vm);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);
        //        return BadRequest(ModelState);
        //    }
        //}


    }
}
