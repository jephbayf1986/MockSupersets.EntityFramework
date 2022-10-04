using MockSupersets.EntityFramework.NetStandard21.Tests.Models;
using System.Data.Entity;

namespace MockSupersets.EntityFramework.NetStandard21.Tests.Contexts
{
    public interface ITestDbContext : IDbContext
    {
        DbSet<Person> People { get; set; }

        DbSet<Payroll> Payrolls { get; set; }

        DbSet<Department> Departments { get; set; }
    }
}