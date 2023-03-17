using System;

using System.Linq;
using Microsoft.EntityFrameworkCore;

public class TypeCar
{
    public int id { get; set; }
    public string name { get; set; }
}

public class Driver
{
    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public DateTime birthdate { get; set; }
}

public class RightCategory
{
    public int id { get; set; }
    public string name { get; set; }
}

public class DriverRightsCategory
{
    public int id_driver { get; set; }
    public int id_rights_category { get; set; }
    public Driver Driver { get; set; }
    public RightCategory RightCategory { get; set; }

}
public class YourDbContext : DbContext
{
    public DbSet<TypeCar> type_car { get; set; }
    public DbSet<Driver> driver { get; set; }
    public DbSet<RightCategory> rights_category { get; set; }
    public DbSet<DriverRightsCategory> driver_rights_category { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverRightsCategory>()
            .HasKey(x => new { x.id_driver, x.id_rights_category });
    }
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }
}

namespace ConsoleApp3
{
    public static class DatabaseRequests
    {
        
        public static void AddTypeCarQuery(string Name)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);
            using (db)
            {
                db.type_car.Add(new TypeCar { name = Name });
                db.SaveChanges();
            }
        }

        public static string GetTypeCarQuery()
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);
            var typeCars = db.type_car.ToList();
            string s = "";
            foreach (var typeCar in typeCars)
            {
                s += $"Id: {typeCar.id} Название: {typeCar.name} \n";
            }
            return s;
        }

        public static void AddDriverQuery(string firstName, string lastName, DateTime birthdate)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);

            var newDriver = new Driver
            {
                first_name = firstName,
                last_name  = lastName,
                birthdate = birthdate
            };

            db.driver.Add(newDriver);
            db.SaveChanges();
        }


        public static string GetDriverQuery()
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);
            var drivers = db.driver.ToList();
            string s = "";

            foreach (var driver in drivers)
            {
                s += $"Id: {driver.id} Имя: {driver.first_name} Фамилия: {driver.last_name} Дата рождения: {driver.birthdate} \n";
            }
            

            return s;
        }
        
        public static void AddRightsCategoryQuery(string Name)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);

            var newRightsCategory = new RightCategory
            {
                name = Name
            };

            db.rights_category.Add(newRightsCategory);
            db.SaveChanges();
        }
        
        public static void GetRightsCategoryQuery()
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);

            var rightsCategories = db.rights_category.ToList();

            foreach (var rightsCategory in rightsCategories)
            {
                Console.WriteLine($"Id: {rightsCategory.id} Название категории прав: {rightsCategory.name}");
            }
        }
        
        public static void AddDriverRightsCategoryQuery(int driverId, int rightsCategoryId)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);

            var newDriverRightsCategory = new DriverRightsCategory
            {
                id_driver = driverId,
                id_rights_category = rightsCategoryId
            };

            db.driver_rights_category.Add(newDriverRightsCategory);
            db.SaveChanges();
        }
        
        public static void GetDriverRightsCategoryQuery()
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseNpgsql(DatabaseService.GetSqlConnection());
            var db = new YourDbContext(optionsBuilder.Options);
            var result = db.driver_rights_category
                .Include(x => x.Driver)
                .Include(x => x.RightCategory)
                .ToList();

            foreach (var driverRightsCategory in result)
            {
                Console.WriteLine($"Имя: {driverRightsCategory.Driver.first_name + driverRightsCategory.Driver.last_name}" +
                                  $" Название категории прав: {driverRightsCategory.RightCategory.name}");
            }
        }

    }
}