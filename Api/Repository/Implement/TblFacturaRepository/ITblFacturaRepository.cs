using Api.Models;
using Api.Repository.Contract;

namespace Api.Repository.Implement.TblFacturaRepository
{
    public interface ITblFacturaRepository : IGenericRepository<TblFactura>
    {
        public Task<IEnumerable<TblFactura>> GetFacturasByCliente(int idCliente);
        public Task<IEnumerable<TblFactura>> GetFacturasByNumeroFactura(int numeroFactura);
    }
}
