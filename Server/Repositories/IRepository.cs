using System.Linq.Expressions;

namespace Server.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? include = null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<T> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
    }

}