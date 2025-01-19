using EzMoq.EfCore;
using EzMoq.EfCore.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EzMoq.EfCore.Tests.Contexts
{
    public interface ITestDbContext : IDbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}