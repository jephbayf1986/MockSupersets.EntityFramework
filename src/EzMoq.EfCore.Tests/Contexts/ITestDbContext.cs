using Microsoft.EntityFrameworkCore;
using MockSupersets.EntityFrameworkCore.Tests.Models;

namespace MockSupersets.EntityFrameworkCore.Tests.Contexts
{
    public interface ITestDbContext : IDbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}