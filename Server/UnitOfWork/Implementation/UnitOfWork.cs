using Server.Data;
using Server.Models;
using Server.Repositories;
using Server.Repositories.Implementation;

namespace Server.UnitOfWork.Implementation
{
    // /src/MvcWithUnitOfWork/UnitOfWork/UnitOfWork.cs
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Countries = new Repository<Country>(context);
            Departments = new Repository<Department>(context);
            Cities = new Repository<City>(context);
            Villages = new Repository<Village>(context);
            Persons = new Repository<Person>(context);
            Deaths = new Repository<Death>(context);
            Relationships = new Repository<Relationship>(context);
        }

        public IRepository<Country> Countries { get; private set; }
        public IRepository<Department> Departments { get; private set; }
        public IRepository<City> Cities { get; private set; }
        public IRepository<Village> Villages { get; private set; }
        public IRepository<Person> Persons { get; private set; }
        public IRepository<Death> Deaths { get; private set; }
        public IRepository<Relationship> Relationships { get; private set; }

        public int Complete() => _context.SaveChanges();

        public Task<int> CompleteAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
