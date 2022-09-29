using MockSupersets.EntityFramework.Tests.Models;
using System.Data.Entity;

namespace MockSupersets.EntityFramework.Tests.Contexts
{
    public class TestDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}