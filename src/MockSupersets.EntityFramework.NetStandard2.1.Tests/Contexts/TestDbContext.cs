using MockSupersets.EntityFramework.NetStandard21.Tests.Models;
using System.Data.Entity;

namespace MockSupersets.EntityFramework.NetStandard21.Tests.Contexts
{
    public class TestDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}