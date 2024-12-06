using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Death> Deaths { get; set; }
        //public DbSet<Parentage> Parentages { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Death)
                .WithOne(d => d.Person)
                .HasForeignKey<Death>(d => d.PersonId);

            modelBuilder.Entity<Person>()
              .HasOne(p => p.Mother)
              .WithMany()
              .HasForeignKey(p => p.MotherId)
              .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Father)
                .WithMany()
                .HasForeignKey(p => p.FatherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relationship>()
              .HasOne(r => r.Father)
              .WithMany(p => p.PaternalFigure)
              .HasForeignKey(r => r.FatherId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.Mother)
                .WithMany(p => p.MaternalFigure)
                .HasForeignKey(r => r.MotherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
               .HasOne(d => d.Country)
               .WithMany(c => c.Departments)
               .HasForeignKey(d => d.CountryId);

            modelBuilder.Entity<City>()
               .HasOne(c => c.Department)
               .WithMany(d => d.Cities)
               .HasForeignKey(c => c.DepartmentId);

            modelBuilder.Entity<Village>()
               .HasOne(v => v.City)
               .WithMany(c => c.Villages)
               .HasForeignKey(v => v.CityId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(c => c.Persons)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Village)
                .WithMany(v => v.Persons)
                .HasForeignKey(p => p.VillageId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Death>()
              .HasOne(d => d.Village)
              .WithMany(c => c.Deaths)
              .HasForeignKey(d => d.VillageId)
              .IsRequired(false);

            modelBuilder.Entity<Death>()
              .HasOne(d => d.City)
              .WithMany(c => c.Deaths)
              .HasForeignKey(d => d.CityId);

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.FullNames);

            //seed data
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Colombia" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Cesar", CountryId = 1 }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Valledupar", DepartmentId = 1 }
            );

            modelBuilder.Entity<Village>().HasData(
                new Village { Id = 1, Name = "Atanquez", CityId = 1 }
            );

            base.OnModelCreating(modelBuilder);
        }

        public static void Seed(ApplicationDbContext context)
        {
            // Verificar si hay datos existentes en las tablas principales para evitar duplicaciones
            if (!context.Countries.Any())
            {
                context.Countries.Add(new Country { Id = 1, Name = "Colombia" });
                context.Departments.Add(new Department { Id = 1, Name = "Cesar", CountryId = 1 });
                context.Cities.Add(new City { Id = 1, Name = "Valledupar", DepartmentId = 1 });
                context.Villages.Add(new Village { Id = 1, Name = "Atanquez", CityId = 1 });
                context.Persons.Add(new Person
                {
                    Id = 1,
                    FirtSurname = "madre",
                    LastSurname = "madre",
                    Description = "Some description",
                    Names = "madre",
                    FullNames = "madre Doe Smith",
                    CityId = 1,
                    VillageId = 1,
                });
                context.Persons.Add(new Person
                {
                    Id = 2,
                    FirtSurname = "padre",
                    LastSurname = "padre",
                    Description = "Some description",
                    Names = "padre",
                    FullNames = "padre Doe Smith",
                    CityId = 1,
                    VillageId = 1,
                });
                context.Persons.Add(new Person
                {
                    Id = 3,
                    FirtSurname = "Hijo",
                    LastSurname = "Smith",
                    Description = "Some description",
                    Names = "Hijo",
                    FullNames = "Hijo Doe Smith",
                    CityId = 1,
                    VillageId = 1,
                });
                //context.Parentages.AddRange(
                //    new Parentage { Id = 1, Name = "Padre" },
                //    new Parentage { Id = 2, Name = "Madre" },
                //    new Parentage { Id = 3, Name = "Hijo" }
                //);

                context.SaveChanges();
            }

            if (context.Persons.Any())
            {
                //context.Persons.Add(new Person
                //{
                //    Id = 1,
                //    FirtSurname = "madre",
                //    LastSurname = "madre",
                //    Description = "Some description",
                //    Names = "madre",
                //    FullNames = "madre Doe Smith",
                //    CityId = 1,
                //    VillageId = 1,
                //});
                //context.Persons.Add(new Person
                //{
                //    Id = 2,
                //    FirtSurname = "padre",
                //    LastSurname = "padre",
                //    Description = "Some description",
                //    Names = "padre",
                //    FullNames = "padre Doe Smith",
                //    CityId = 1,
                //    VillageId = 1,
                //});
                //context.Persons.Add(new Person
                //{
                //    Id = 4,
                //    FirtSurname = "Hijo 2",
                //    LastSurname = "Smith",
                //    Description = "Some description",
                //    Names = "Hijo 2",
                //    FullNames = "Hijo 2 Doe Smith",
                //    CityId = 1,
                //    VillageId = 1,
                //});

                context.SaveChanges();
            }
        }
    }
}
