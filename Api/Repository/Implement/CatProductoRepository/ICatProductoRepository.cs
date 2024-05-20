
using Api.Models;
using Api.Repository.Contract;

namespace Api.Repository.Implement.CatProductoRepository
{
    public interface ICatProductoRepository : IGenericRepository<CatProducto>
    {

        Task<CatProducto?> Update(CatProducto entity);
        Task<CatProducto?> Delete(int id);
    }
}