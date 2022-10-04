using MockSupersets.EntityFramework.NetStandard21.Tests.Contexts;
using MockSupersets.EntityFramework.NetStandard21.Tests.Models;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MockSupersets.EntityFramework.Tests.MockDbContext
{
    public class VerifySaveTests
    {
        [Fact]
        public void VerifyChangesSavedShouldNotThrowWhenSaved()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            // Act
            sut.SaveChanges();

            // Assert
            mock.VerifyChangesSaved();
        }

        [Fact]
        public async Task VerifyChangesSavedAsyncShouldNotThrowWhenSavedAsyncWithNoParamaters()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            // Act
            await sut.SaveChangesAsync();

            // Assert
            mock.VerifyChangesSavedAsync();
        }

        [Fact]
        public async Task VerifyChangesSavedAsyncShouldNotThrowWhenSavedAsyncWithCancellationToken()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            // Act
            await sut.SaveChangesAsync(new CancellationToken());

            // Assert
            mock.VerifyChangesSavedAsync();
        }

        [Fact]
        public void VerifyChangesNotSavedShouldNotThrowWhenNotSaved()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            // Act

            // Assert
            mock.VerifyChangesNotSaved();
        }

        [Fact]
        public void VerifyChangesNotSavedShouldNotThrowWhenNotSavedAsync()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            // Act

            // Assert
            mock.VerifyChangesNotSavedAsync();
        }
    }
}