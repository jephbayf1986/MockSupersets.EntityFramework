namespace MockSupersets.EntityFrameworkCore.Tests.Models
{
    internal class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Height { get; set; }

        public int PayrollNumber { get; set; }
    }
}