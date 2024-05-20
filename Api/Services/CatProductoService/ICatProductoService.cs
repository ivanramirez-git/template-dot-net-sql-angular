using Api.Models;

namespace Api.Services.CatProductoService
{
    public interface ICatProductoService
    {
        // Get All
        public IEnumerable<CatProducto> Get();
        // Get By Id
        public CatProducto? Get(int id);
        // Create
        public CatProducto? Create(CatProducto catProducto);
        // Update
        public CatProducto? Update(CatProducto catProducto);
        // Delete
        public CatProducto? Delete(int id);
    }
}