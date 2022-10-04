using MockSupersets.EntityFramework.NetStandard21.Tests.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static dotRandom.DotRandom;
using MockSupersets.EntityFramework.NetStandard21.Tests.Contexts;

namespace MockSupersets.EntityFramework.NetStandard21.Tests.MockDbContext
{
    public class VerifyActionTests
    {
        [Fact]
        public void VerifyAddedShouldNotThrowWhenAdded()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
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
        public void VerifyRangeAddedShouldNotThrowWhenAdded()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            Person person1 = new Person() { FirstName = RandomString(20), LastName = RandomString(20) };
            Person person2 = new Person() { FirstName = RandomString(20), LastName = RandomString(20) };
            Person person3 = new Person() { FirstName = RandomString(20), LastName = RandomString(20) };

            var newPeople = new List<Person>() { person1, person2, person3 };

            // Act
            sut.People.AddRange(newPeople);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Count() == newPeople.Count());
        }

        [Fact]
        public void VerifyUpdatedShouldNotThrowWhenUpdated()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var newFirstName = RandomString(25);
            var newLastName = RandomString(25);

            Person person = sut.People.FirstOrDefault();

            // Act
            person.FirstName = newFirstName;
            person.LastName = newLastName;

            // Assert
            mock.VerifyUpdated<Person>(x => x.FirstName == newFirstName
                                         && x.LastName == newLastName);
        }

        [Fact]
        public void VerifyRemovedShouldNotThrowWhenRemoved()
        {
            // Arrange
            var id = RandomPostitiveInt();
            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person personToRemove = new Person() { Id = id, FirstName = firstName, LastName = lastName };

            var mock = new MockDbContext<TestDbContext>()
                                .WithEntities(personToRemove);

            var sut = mock.Object;

            // Act
            sut.People.Remove(personToRemove);

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                         && x.LastName == lastName);
        }

        [Fact]
        public void VerifyRangeRemovedShouldNotThrowWhenRemoved()
        {
            // Arrange
            Person person1 = new Person() { Id = 1, FirstName = RandomString(20), LastName = RandomString(20) };
            Person person2 = new Person() { Id = 2, FirstName = RandomString(20), LastName = RandomString(20) };
            Person person3 = new Person() { Id = 3, FirstName = RandomString(20), LastName = RandomString(20) };

            var mock = new MockDbContext<TestDbContext>()
                                .WithEntities(person1, person2, person3);

            var sut = mock.Object;

            var peopleToRemoved = sut.People.Where(x => x.Id <= 3);

            // Act
            sut.People.RemoveRange(peopleToRemoved);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Count() == peopleToRemoved.Count());
        }

        [Fact]
        public void VerifyNotAddedShouldNotThrowWhenNotAdded()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>()
                                .WithEntity<Department>();

            var sut = mock.Object;

            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Add(newPerson);

            // Assert
            mock.VerifyNotAdded<Department>(x => x != null);
        }

        [Fact]
        public void VerifyRangeNotAddedShouldNotThrowWhenNotAdded()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>()
                                .WithEntity<Department>();

            var sut = mock.Object;

            Person person1 = new Person() { FirstName = RandomString(20), LastName = RandomString(20) };
            Person person2 = new Person() { FirstName = RandomString(20), LastName = RandomString(20) };
            Person person3 = new Person() { FirstName = RandomString(20), LastName = RandomString(20) };

            var newPeople = new List<Person>() { person1, person2, person3 };

            // Act
            sut.People.AddRange(newPeople);

            // Assert
            mock.VerifyRangeNotAdded<Department>(x => x.Count() > 0);
        }

        [Fact]
        public void VerifyNotRemovedShouldNotThrowWhenNotRemoved()
        {
            // Arrange
            var id = RandomPostitiveInt();
            var firstName = RandomString(20);
            var lastName = RandomString(20);

            Person personToRemove = new Person() { Id = id, FirstName = firstName, LastName = lastName };

            var mock = new MockDbContext<TestDbContext>()
                                .WithEntities(personToRemove)
                                .WithEntity<Department>();

            var sut = mock.Object;

            // Act
            sut.People.Remove(personToRemove);

            // Assert
            mock.VerifyNotRemoved<Department>(x => x != null);
        }

        [Fact]
        public void VerifyRangeNotRemovedShouldNotThrowWhenNotRemoved()
        {
            // Arrange
            Person person1 = new Person() { Id = 1, FirstName = RandomString(20), LastName = RandomString(20) };
            Person person2 = new Person() { Id = 2, FirstName = RandomString(20), LastName = RandomString(20) };
            Person person3 = new Person() { Id = 3, FirstName = RandomString(20), LastName = RandomString(20) };

            var mock = new MockDbContext<TestDbContext>()
                                .WithEntities(person1, person2, person3)
                                .WithEntity<Department>();

            var sut = mock.Object;

            var peopleToRemoved = sut.People.Where(x => x.Id <= 3);

            // Act
            sut.People.RemoveRange(peopleToRemoved);

            // Assert
            mock.VerifyRangeNotRemoved<Department>(x => x.Count() > 0);
        }
    }
}