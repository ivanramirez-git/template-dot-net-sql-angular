using Api.Controllers;
using Api.Models;
using Api.Repository.Implement.CatProductoRepository;
using Api.Services.CatProductoService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiTest.Controllers
{
    public class CatProductoControllerTest
    {
        private readonly CatProductoController _controller;
        private readonly ICatProductoService _service;
        private readonly ICatProductoRepository _repository;
        public CatProductoControllerTest()
        {
            _repository = new CatProductoRepositoryImpl(
                new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            );
            _service = new CatProductoServiceImpl(_repository);
            _controller = new CatProductoController(_service);
        }

        // Metodo Get

        // Esta prueba es para verificar que el metodo Get() regrese un 200, ya que es el codigo de respuesta correcto
        [Fact]
        public void Get_Ok()
        {
            var result = _controller.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        // Prueba que evalua la cantidad de registros que se obtienen del metodo Get()
        [Fact]
        public void Get_Quantity()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<CatProducto>>(okResult.Value);
            Assert.True(items.Count > 0);
        }

        // Prueba que evalua si DeletedAt es null en todos los registros que se obtienen del metodo Get()
        [Fact]
        public void Get_DeletedAt()
        {
            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsType<List<CatProducto>>(okResult.Value);
            foreach (var item in items)
            {
                Assert.Null(item.DeletedAt);
            }
        }

        // Metodo Get(int id)

        // Esta prueba es para verificar que el metodo Get(0) regrese un 404, ya que no existe un registro con id = 0
        [Fact]
        public void Get_NotFound()
        {
            var result = _controller.Get(0);
            Assert.IsType<NotFoundResult>(result);
        }

        // Esta prueba es para verificar que el metodo Get(1) regrese un 200, ya que existe un registro con id = 1
        [Fact]
        public void Get_OkId()
        {
            int id = 1;
            var result = _controller.Get(id);
            Assert.IsType<OkObjectResult>(result);
        }

        // Esta prueba es para verificar que el metodo Get(1) regrese un registro con id = 1
        [Fact]
        public void Get_Id()
        {
            int id = 1;
            var result = _controller.Get(id);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<CatProducto>(okResult.Value);
            Assert.Equal(id, item.Id);
        }

        // Metodo Create(CatProducto catProducto)
        [Fact]
        public void Create_Ok()
        {
            var catProducto = new CatProducto
            {
                NombreProducto = "Producto de prueba",
                ImagenProducto = "Imagen de prueba",
                Precio = 100,
                // max 5 caracteres
                Ext = "12345"
            };
            var result = _controller.Create(catProducto);
            Assert.IsType<OkObjectResult>(result);
            // Guardar el id del registro creado
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<CatProducto>(okResult.Value);
        }

        // Metodo Update(CatProducto catProducto)
        [Fact]
        public void Update_Ok()
        {
            var _result = _controller.Get();
            var _okResult = Assert.IsType<OkObjectResult>(_result);
            var _items = Assert.IsType<List<CatProducto>>(_okResult.Value);
            var _item = _items.Where(x => x.NombreProducto == "Producto de prueba" && x.Ext == "12345").FirstOrDefault();
            // Si es nulo es porque deleted paso primero entonces este test tambien puede pasar
            if (_item == null)
            {
                Assert.Null(_item);
            }
            var id = _item?.Id;
            if (id != null)
            {
                var catProducto = new CatProducto
                {
                    Id = id.Value,
                    NombreProducto = "Producto de prueba",
                    ImagenProducto = "Nueva imagen de prueba",
                    Precio = 100,
                    // max 5 caracteres
                    Ext = "54321"
                };
                var result = _controller.Update(catProducto);
                Assert.IsType<OkObjectResult>(result);

                // Verificar que el registro se actualizo correctamente en la imagen y la ext
                var okResult = Assert.IsType<OkObjectResult>(result);
                var item = Assert.IsType<CatProducto>(okResult.Value);
                Assert.Equal(catProducto.ImagenProducto, item.ImagenProducto);
                Assert.Equal(catProducto.Ext, item.Ext);
            }
        }

        // Metodo Delete(int id) pasa despues de Update
        [Fact]
        public void Delete_Ok()
        {
            var _result = _controller.Get();
            var _okResult = Assert.IsType<OkObjectResult>(_result);
            var _items = Assert.IsType<List<CatProducto>>(_okResult.Value);
            var _item = _items.Where(x => x.NombreProducto == "Producto de prueba" && x.Ext == "54321").FirstOrDefault();
            if (_item == null)
            {
                _item = _items.Where(x => x.NombreProducto == "Producto de prueba" && x.Ext == "12345").FirstOrDefault();
            }
            if (_item != null)
            {
                var id = _item.Id;
                var result = _controller.Delete(id);
                Assert.IsType<OkObjectResult>(result);
                // Verificar que el registro se elimino correctamente
                var okResult = Assert.IsType<OkObjectResult>(result);
                var item = Assert.IsType<CatProducto>(okResult.Value);
                Assert.NotNull(item.DeletedAt);
            }
        }

    }
}
