using System;

namespace MockSupersets.EntityFramework.NetStandard21.Tests.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOpen { get; set; }

        public DateTime? DateClosed { get; set; }
    }
}