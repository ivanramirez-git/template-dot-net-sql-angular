using Api.Models;
using Api.Repository.Implement.CatTipoClienteRepository;

namespace Api.Services.CatTipoClienteService
{
    public class CatTipoClienteServiceImpl : ICatTipoClienteService
    {
        private readonly ICatTipoClienteRepository _repository;

        public CatTipoClienteServiceImpl(ICatTipoClienteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CatTipoCliente> Get()
        {
            return _repository.GetAll().Result;
        }

        public CatTipoCliente? Get(int id)
        {
            return _repository.GetById(id).Result;
        }

        public CatTipoCliente? Create(CatTipoCliente catTipoCliente)
        {
            return _repository.Create(catTipoCliente).Result;
        }

        public CatTipoCliente? Update(CatTipoCliente catTipoCliente)
        {
            return _repository.Update(catTipoCliente).Result;
        }

        public CatTipoCliente? Delete(int id)
        {
            return _repository.Delete(id).Result;
        }

    }
}