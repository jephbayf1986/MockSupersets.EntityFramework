using Microsoft.EntityFrameworkCore;
using MockSupersets.EntityFrameworkCore.Tests.Models;

namespace MockSupersets.EntityFrameworkCore.Tests.Contexts
{
    internal class TestDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}