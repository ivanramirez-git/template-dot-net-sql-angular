// Imports Framework
using Microsoft.AspNetCore.Mvc;

// Imports Project
using Api.Services.TblFacturaService;
using Api.Models;

namespace Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class TblFacturaController : ControllerBase
    {
        private readonly ITblFacturaService _service;
        public TblFacturaController(ITblFacturaService service)
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
        public IActionResult Create(TblFactura factura)
        {
            var result = _service.Create(factura);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("TblCliente/{idCliente}")]
        public IActionResult GetFacturasByCliente(int idCliente)
        {
            var catTipoCliente = _service.GetFacturasByCliente(idCliente);
            if (catTipoCliente == null)
                return NotFound();
            return Ok(catTipoCliente);
        }

        [HttpGet("NumeroFactura/{numeroFactura}")]
        public IActionResult GetFacturasByNumeroFactura(int numeroFactura)
        {
            var catTipoCliente = _service.GetFacturasByNumeroFactura(numeroFactura);
            if (catTipoCliente == null)
                return NotFound();
            return Ok(catTipoCliente);
        }
    }
}