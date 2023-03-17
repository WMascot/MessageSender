using MessageSender.DAL.Context;
using MessageSender.DAL.Interfaces;
using MessageSender.DAL.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace MessageSender.DAL.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _set;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }
        public virtual IQueryable<T> Items => _set.AsNoTracking();

        public T Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
            return entity;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Added;
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public T? Get(int id) => Items.FirstOrDefault(x => x.Id == id);
        public async Task<T?> GetAsync(int id, CancellationToken cancellationToken) => 
            await Items.FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);

        public void Remove(int id)
        {
            var entity = Items.FirstOrDefault(x => x.Id == id) ?? new T { Id = id };
            _set.Local.Remove(entity);
            _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await Items.FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false) ?? new T { Id = id };
            _set.Local.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public T Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }
    }
}
