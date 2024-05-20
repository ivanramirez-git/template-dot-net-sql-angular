using Api.Models;

namespace Api.Services.CatTipoClienteService
{
    public interface ICatTipoClienteService
    {
        // Get All
        public IEnumerable<CatTipoCliente> Get();
        // Get By Id
        public CatTipoCliente? Get(int id);
        // Create
        public CatTipoCliente? Create(CatTipoCliente catTipoCliente);
        // Update
        public CatTipoCliente? Update(CatTipoCliente catTipoCliente);
        // Delete
        public CatTipoCliente? Delete(int id);
    }
}