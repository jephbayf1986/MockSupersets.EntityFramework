namespace MockSupersets.EntityFramework.Common
{
    public class MockDbContextOptions
    {
        public bool AutoPopulateDbSets { get; set; } = false;

        public int MinItemsInDbSet { get; set; } = 5;
    }
}