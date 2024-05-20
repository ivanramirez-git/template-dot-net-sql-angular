using Api.Models;
using Api.Repository.Implement.TblFacturaRepository;

namespace Api.Services.TblFacturaService
{
    public class TblFacturaServiceImpl : ITblFacturaService
    {
        private readonly ITblFacturaRepository _tblFacturaRepository;

        public TblFacturaServiceImpl(ITblFacturaRepository tblFacturaRepository)
        {
            _tblFacturaRepository = tblFacturaRepository;
        }

        public IEnumerable<TblFactura> Get()
        {
            return _tblFacturaRepository.GetAll().Result;
        }

        public TblFactura? Get(int id)
        {
            return _tblFacturaRepository.GetById(id).Result;
        }

        public TblFactura? Create(TblFactura tblFactura)
        {
            return _tblFacturaRepository.Create(tblFactura).Result;
        }

        public IEnumerable<TblFactura> GetFacturasByCliente(int idCliente)
        {
            return _tblFacturaRepository.GetFacturasByCliente(idCliente).Result;
        }

        public IEnumerable<TblFactura> GetFacturasByNumeroFactura(int numeroFactura)
        {
            return _tblFacturaRepository.GetFacturasByNumeroFactura(numeroFactura).Result;
        }

    }
}
