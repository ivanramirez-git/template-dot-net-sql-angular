using Api.Models;
using Api.Repository.Implement.TblDetalleFacturaRepository;

namespace Api.Services.TblDetalleFacturaService
{
    public class TblDetalleFacturaServiceImpl : ITblDetalleFacturaService
    {
        private readonly ITblDetalleFacturaRepository _tblDetalleFacturaRepository;

        public TblDetalleFacturaServiceImpl(ITblDetalleFacturaRepository tblDetalleFacturaRepository)
        {
            _tblDetalleFacturaRepository = tblDetalleFacturaRepository;
        }

        public IEnumerable<TblDetalleFactura> Get()
        {
            return _tblDetalleFacturaRepository.GetAll().Result;
        }

        public TblDetalleFactura? Get(int id)
        {
            return _tblDetalleFacturaRepository.GetById(id).Result;
        }

        public TblDetalleFactura? Create(TblDetalleFactura tblDetalleFactura)
        {
            return _tblDetalleFacturaRepository.Create(tblDetalleFactura).Result;
        }
    }
}