using Moq;
using System.Linq;
using System.Reflection;

namespace MockSupersets.EntityFramework.Common.Helpers
{
    internal static class MockHelper
    {
        public static Mock<T> GetMockFromObject<T>(this T mockedObject) where T : class
        {
            PropertyInfo[] pis = mockedObject.GetType().GetProperties()
                .Where(
                    p => p.PropertyType.Name == "Mock`1"
                ).ToArray();

            return pis.First().GetGetMethod().Invoke(mockedObject, null) as Mock<T>;
        }
    }
}