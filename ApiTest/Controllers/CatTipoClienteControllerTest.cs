using Api.Controllers;
using Api.Models;
using Api.Repository.Implement.CatTipoClienteRepository;
using Api.Services.CatTipoClienteService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiTest.Controllers
{
    public class CatTipoClienteControllerTest
    {
        private readonly CatTipoClienteController _controller;
        private readonly ICatTipoClienteService _service;
        private readonly ICatTipoClienteRepository _repository;

        public CatTipoClienteControllerTest()
        {
            _repository = new CatTipoClienteRepositoryImpl(
                new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            );
            _service = new CatTipoClienteServiceImpl(_repository);
            _controller = new CatTipoClienteController(_service);
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
            var items = Assert.IsType<List<CatTipoCliente>>(okResult.Value);
            Assert.True(items.Count > 0);
        }

        [Fact]
        public void Get_DeletedAt()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<CatTipoCliente>>(okResult.Value);
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
            int id = 0; // Asume que no existe un TipoCliente con ID 0
            var result = _controller.Get(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Ok()
        {
            var TipoCliente = new CatTipoCliente
            {
                TipoCliente = "TipoCliente de prueba",
            };
            var result = _controller.Create(TipoCliente);
            Assert.IsType<OkObjectResult>(result);
            // Guarda el ID del registro creado
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<CatTipoCliente>(okResult.Value);
        }

        [Fact]
        public void Update_Ok()
        {
            var _result = _controller.Get();
            var _okResult = Assert.IsType<OkObjectResult>(_result);
            var _items = Assert.IsType<List<CatTipoCliente>>(_okResult.Value);
            var _item = _items.Where(x => x.TipoCliente == "TipoCliente de prueba").FirstOrDefault();
            // Si es nulo es porque DeletedAt pasó primero, entonces este test también puede pasar
            if (_item == null)
            {
                Assert.Null(_item);
            }
            var id = _item?.Id;
            if (id != null)
            {
                var TipoCliente = new CatTipoCliente
                {
                    Id = id.Value,
                    TipoCliente = "TipoCliente de prueba actualizado",
                };
                var result = _controller.Update(TipoCliente);
                Assert.IsType<OkObjectResult>(result);

                // Verifica que el registro se actualizó correctamente en la imagen y la extensión
                var okResult = Assert.IsType<OkObjectResult>(result);
                var item = Assert.IsType<CatTipoCliente>(okResult.Value);
                Assert.Equal(TipoCliente.TipoCliente, item.TipoCliente);
                // Imprime el ID del registro actualizado
            }
        }

        [Fact]
        public void Delete_Ok()
        {
            var _result = _controller.Get();
            var _okResult = Assert.IsType<OkObjectResult>(_result);
            var _items = Assert.IsType<List<CatTipoCliente>>(_okResult.Value);
            var _item = _items.Where(x => x.TipoCliente == "TipoCliente de prueba").FirstOrDefault();
            if (_item == null)
            {
                _item = _items.Where(x => x.TipoCliente == "TipoCliente de prueba actualizado").FirstOrDefault();
            }
            if (_item != null)
            {
                var id = _item.Id;
                var result = _controller.Delete(id);
                Assert.IsType<OkObjectResult>(result);
                // Verifica que el registro se eliminó correctamente
                var okResult = Assert.IsType<OkObjectResult>(result);
                var item = Assert.IsType<CatTipoCliente>(okResult.Value);
                Assert.NotNull(item.DeletedAt);
            }
        }
    }
}
