namespace EzMoq.EfCore.Tests.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public decimal Height { get; set; }

        public int PayrollNumber { get; set; }
    }
}