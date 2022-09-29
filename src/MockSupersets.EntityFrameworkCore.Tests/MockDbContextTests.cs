using MockSupersets.EntityFrameworkCore;
using MockSupersets.EntityFrameworkCore.Tests.Contexts;
using MockSupersets.EntityFrameworkCore.Tests.Models;

namespace MockSupersets.EntityFramework.Tests
{
    public class MockDbContextTests
    {
        [Fact]
        public void VerifyAddedShouldNotThrowWhenAdded()
        {
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;
        }
    }
}