using Api.Models;
using Api.Repository.Implement.TblClienteRepository;

namespace Api.Services.TblClienteService
{
    public class TblClienteServiceImpl : ITblClienteService
    {
        private readonly ITblClienteRepository _tblClienteRepository;

        public TblClienteServiceImpl(ITblClienteRepository tblClienteRepository)
        {
            _tblClienteRepository = tblClienteRepository;
        }

        public IEnumerable<TblCliente> Get()
        {
            return _tblClienteRepository.GetAll().Result;
        }

        public TblCliente? Get(int id)
        {
            return _tblClienteRepository.GetById(id).Result;
        }

        public TblCliente? Create(TblCliente tblCliente)
        {
            return _tblClienteRepository.Create(tblCliente).Result;
        }

        public TblCliente? Update(TblCliente tblCliente)
        {
            return _tblClienteRepository.Update(tblCliente).Result;
        }

        public TblCliente? Delete(int id)
        {
            return _tblClienteRepository.Delete(id).Result;
        }
        
    }
}