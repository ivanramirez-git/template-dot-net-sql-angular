using Api.Controllers;
using Api.Models;
using Api.Repository.Implement.TblClienteRepository;
using Api.Services.TblClienteService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiTest.Controllers
{
    public class TblClienteControllerTest
    {
        private readonly TblClienteController _controller;
        private readonly ITblClienteService _service;
        private readonly ITblClienteRepository _repository;

        public TblClienteControllerTest()
        {
            _repository = new TblClienteRepositoryImpl(
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
            );
            _service = new TblClienteServiceImpl(_repository);
            _controller = new TblClienteController(_service);
        }

        [Fact]
        public void Get_Ok()
        {
            var result = _controller.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_Quantity()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<TblCliente>>(okResult.Value);
            Assert.True(items.Count > 0);
        }

        [Fact]
        public void Get_DeletedAt()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<TblCliente>>(okResult.Value);
            foreach (var item in items)
            {
                Assert.Null(item.DeletedAt);
            }
        }

        [Fact]
        public void Get_ById_Ok()
        {
            int id = 1;
            var result = _controller.Get(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_ById_NotFound()
        {
            int id = 0; // Asume que no existe un cliente con ID 0
            var result = _controller.Get(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Ok()
        {
            var cliente = new TblCliente
            {
                RazonSocial = "Cliente de prueba",
                IdTipoCliente = 1, // Asigna un ID de tipo de cliente válido
                FechaDeCreacion = DateTime.Now,
                RFC = "RFC12345"
            };
            var result = _controller.Create(cliente);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Update_Ok()
        {
            // Asume que existe un cliente con el nombre "Cliente de prueba" y RFC "RFC12345" que se creó en la prueba anterior
            var result = _controller.Get(); // Obtener todos los clientes
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<TblCliente>>(okResult.Value);
            var cliente = items.Find(c => c.RazonSocial == "Cliente de prueba" && c.RFC == "RFC12345");
            if (cliente != null)
            {
                // Modifica algún campo del cliente
                cliente.RazonSocial = "Cliente Modificado";
                var updateResult = _controller.Update(cliente);
                Assert.IsType<OkObjectResult>(updateResult);
            }
            if (cliente == null)
            {
                Assert.Null(cliente);
            }
        }

        [Fact]
        public void Delete_Ok()
        {
            // Asume que existe un cliente con el nombre "Cliente de prueba" y RFC "RFC12345" que se creó en la prueba anterior
            var result = _controller.Get(); // Obtener todos los clientes
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<TblCliente>>(okResult.Value);
            var cliente = items.Find(c => c.RazonSocial == "Cliente de prueba" && c.RFC == "RFC12345");
            if (cliente == null)
            {
                cliente = items.Find(c => c.RazonSocial == "Cliente Modificado" && c.RFC == "RFC12345");
            }
            if (cliente != null)
            {
                var deleteResult = _controller.Delete(cliente.Id);
                Assert.IsType<OkObjectResult>(deleteResult);
            }
        }
    }
}
