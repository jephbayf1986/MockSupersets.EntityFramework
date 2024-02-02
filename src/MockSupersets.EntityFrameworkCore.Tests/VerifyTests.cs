using MockSupersets.EntityFrameworkCore;
using MockSupersets.EntityFrameworkCore.Tests.Contexts;
using MockSupersets.EntityFrameworkCore.Tests.Models;

namespace MockSupersets.EntityFramework.Tests
{
    public class VerifyTests
    {
        [Fact]
        public void WhenAddedOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Add(newPerson);

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName 
                                       && x.LastName == lastName);
        }

        [Fact]
        public void WhenAddedDirect_ShouldVerifyAddedCorrectly()
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
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void WhenNotAdded_ShouldVerifyNotAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.AddAsync(newPerson);

            // Assert
            mock.VerifyNotAdded<Person>(x => x.FirstName == firstName
                                          && x.LastName == lastName);
        }
    }
}