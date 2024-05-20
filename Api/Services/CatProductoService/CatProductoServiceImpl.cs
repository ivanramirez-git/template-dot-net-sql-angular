using Api.Models;
using Api.Repository.Contract;
using Api.Repository.Implement.CatProductoRepository;

namespace Api.Services.CatProductoService
{
    public class CatProductoServiceImpl : ICatProductoService
    {
        private readonly ICatProductoRepository _repository;

        public CatProductoServiceImpl(ICatProductoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CatProducto> Get()
        {
            return _repository.GetAll().Result;
        }

        public CatProducto? Get(int id)
        {
            return _repository.GetById(id).Result;
        }

        public CatProducto? Create(CatProducto catProducto)
        {
            return _repository.Create(catProducto).Result;
        }

        public CatProducto? Update(CatProducto catProducto)
        {
            return _repository.Update(catProducto).Result;
        }

        public CatProducto? Delete(int id)
        {
            return _repository.Delete(id).Result;
        }
    }
}