// Imports Framework
using Microsoft.AspNetCore.Mvc;

// Imports Project
using Api.Services.CatProductoService;
using Api.Models;

namespace Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CatProductoController : ControllerBase
    {
        private readonly ICatProductoService _service;
        public CatProductoController(ICatProductoService catProducto)
        {
            _service = catProducto;
        }
        [HttpGet]
        public IActionResult Get() => Ok(_service.Get());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var catProducto = _service.Get(id);
            if (catProducto == null)
                return NotFound();

            return Ok(catProducto);
        }

        [HttpPost]
        public IActionResult Create(CatProducto catProducto)
        {
            var result = _service.Create(catProducto);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(CatProducto catProducto)
        {
            var result = _service.Update(catProducto);
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