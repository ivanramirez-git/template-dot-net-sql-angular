// Imports Framework
using Microsoft.AspNetCore.Mvc;

// Imports Project
using Api.Services.CatTipoClienteService;
using Api.Models;

namespace Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CatTipoClienteController : ControllerBase
    {
        private readonly ICatTipoClienteService _service;
        public CatTipoClienteController(ICatTipoClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.Get());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var catTipoCliente = _service.Get(id);
            if (catTipoCliente == null)
                return NotFound();

            return Ok(catTipoCliente);
        }

        [HttpPost]
        public IActionResult Create(CatTipoCliente catTipoCliente)
        {
            var result = _service.Create(catTipoCliente);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(CatTipoCliente catTipoCliente)
        {
            var result = _service.Update(catTipoCliente);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
        
    }
}