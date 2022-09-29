namespace MockSupersets.EntityFrameworkCore.Tests.Models
{
    internal class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOpen { get; set; }

        public DateTime? DateClosed { get; set; }
    }
}