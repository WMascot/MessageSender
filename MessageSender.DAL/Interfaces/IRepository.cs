namespace MessageSender.DAL.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Items { get; }
        T? Get(int id);
        Task<T?> GetAsync(int id, CancellationToken cancellationToken);
        T Add(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        T Update(T entity);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        void Remove(int id);
        Task RemoveAsync(int id, CancellationToken cancellationToken);
    }
}
