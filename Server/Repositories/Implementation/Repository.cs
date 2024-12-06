using Microsoft.EntityFrameworkCore;
using Server.Data;
using System.Linq.Expressions;

namespace Server.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public T Get(int id) => _entities.Find(id);

        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public IQueryable<T> GetAll() => _entities.AsQueryable();

        public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _entities.Where(predicate);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
                                            Func<IQueryable<T>, IQueryable<T>> include = null!)
        {
            IQueryable<T> query = _entities.Where(predicate);

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            // Agrega las relaciones (llaves foráneas) especificadas en los includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Busca la entidad con el ID
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }


        public void Add(T entity) => _entities.Add(entity);

        public void AddRange(IEnumerable<T> entities) => _entities.AddRange(entities);

        public void Update(T entity) => _entities.Update(entity);

        public void Remove(T entity) => _entities.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => _entities.RemoveRange(entities);
    }

}
