using Microsoft.EntityFrameworkCore;
using MockSupersets.EntityFrameworkCore.Tests.Models;

namespace MockSupersets.EntityFrameworkCore.Tests.Contexts
{
    internal interface ITestDbContext : IDbContext
    {
        DbSet<Person> People { get; set; }

        DbSet<Payroll> Payrolls { get; set; }

        DbSet<Department> Departments { get; set; }
    }
}