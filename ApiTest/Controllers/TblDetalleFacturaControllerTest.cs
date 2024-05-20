using Api.Controllers;
using Api.Models;
using Api.Repository.Implement.TblDetalleFacturaRepository;
using Api.Services.TblDetalleFacturaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiTest.Controllers
{
    public class TblDetalleFacturaControllerTest
    {
        private readonly TblDetalleFacturaController _controller;
        private readonly ITblDetalleFacturaService _service;
        private readonly ITblDetalleFacturaRepository _repository;

        public TblDetalleFacturaControllerTest()
        {
            _repository = new TblDetalleFacturaRepositoryImpl(
                new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            );
            _service = new TblDetalleFacturaServiceImpl(_repository);
            _controller = new TblDetalleFacturaController(_service);
        }

        [Fact]
        public void Get_Ok()
        {
            var result = _controller.Get();
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
