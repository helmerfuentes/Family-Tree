using Server.Models;
using Server.Repositories;

namespace Server.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Country> Countries { get; }
        IRepository<Department> Departments { get; }
        IRepository<City> Cities { get; }
        IRepository<Village> Villages { get; }
        IRepository<Person> Persons { get; }
        IRepository<Death> Deaths { get; }
        IRepository<Relationship> Relationships { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
