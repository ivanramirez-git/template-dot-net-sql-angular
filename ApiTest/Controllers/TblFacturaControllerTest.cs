using Api.Controllers;
using Api.Models;
using Api.Repository.Implement.TblFacturaRepository;
using Api.Services.TblFacturaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace ApiTest.Controllers
{
    public class TblFacturaControllerTest
    {
        private readonly TblFacturaController _controller;
        private readonly ITblFacturaService _service;
        private readonly ITblFacturaRepository _repository;

        public TblFacturaControllerTest()
        {
            _repository = new TblFacturaRepositoryImpl(
                new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            );
            _service = new TblFacturaServiceImpl(_repository);
            _controller = new TblFacturaController(_service);
        }

        [Fact]
        public void Get_Ok()
        {
            var result = _controller.Get();
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
