using EzMoq.EfCore.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EzMoq.EfCore.Tests.Contexts
{
    public class TestDbContext : DbContext, ITestDbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}