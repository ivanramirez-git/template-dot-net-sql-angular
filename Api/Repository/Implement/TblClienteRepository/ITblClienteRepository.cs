using Api.Models;
using Api.Repository.Contract;

namespace Api.Repository.Implement.TblClienteRepository
{
    public interface ITblClienteRepository : IGenericRepository<TblCliente>
    {
        Task<TblCliente?> Update(TblCliente entity);
        Task<TblCliente?> Delete(int id);
    }
}
