using EzMoq.EfCore.Tests.Contexts;
using EzMoq.EfCore.Tests.Models;

namespace EzMoq.EfCore.Tests
{
    public class VerifyTests
    {
        [Fact]
        public void GivenInterface_WhenAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Add(newPerson);

            // Assert
            mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenClass_WhenAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Add(newPerson);

            // Assert
            mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenInterface_WhenNotAdded_ShouldVerifyNotAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.AddAsync(newPerson);

            // Assert
            mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                          && x.LastName == lastName);
        }

        [Fact]
        public void GivenClass_WhenNotAdded_ShouldVerifyNotAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.AddAsync(newPerson);

            // Assert
            mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                          && x.LastName == lastName);
        }
    }
}