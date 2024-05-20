using Api.Models;

namespace Api.Services.TblDetalleFacturaService
{
    public interface ITblDetalleFacturaService
    {
        // Get All
        public IEnumerable<TblDetalleFactura> Get();
        // Get By Id
        public TblDetalleFactura? Get(int id);
        // Create
        public TblDetalleFactura? Create(TblDetalleFactura tblDetalleFactura);
    }
}