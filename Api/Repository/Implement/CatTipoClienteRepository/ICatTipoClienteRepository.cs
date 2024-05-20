using Api.Models;
using Api.Repository.Contract;

namespace Api.Repository.Implement.CatTipoClienteRepository
{
    public interface ICatTipoClienteRepository : IGenericRepository<CatTipoCliente>
    {
        Task<CatTipoCliente?> Update(CatTipoCliente entity);
        Task<CatTipoCliente?> Delete(int id);
    }
}
