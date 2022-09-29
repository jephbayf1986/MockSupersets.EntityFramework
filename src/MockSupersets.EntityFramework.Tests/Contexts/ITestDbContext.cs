using MockSupersets.EntityFramework.Tests.Models;
using System.Data.Entity;

namespace MockSupersets.EntityFramework.Tests.Contexts
{
    public interface ITestDbContext : IDbContext
    {
        DbSet<Person> People { get; set; }

        DbSet<Payroll> Payrolls { get; set; }

        DbSet<Department> Departments { get; set; }
    }
}