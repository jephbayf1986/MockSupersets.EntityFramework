namespace EzMoq.EfCore.Tests.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime DateOpen { get; set; }

        public DateTime? DateClosed { get; set; }
    }
}