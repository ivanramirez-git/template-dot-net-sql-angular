namespace Api.Repository.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T?> Create(T entity);
    }
}
