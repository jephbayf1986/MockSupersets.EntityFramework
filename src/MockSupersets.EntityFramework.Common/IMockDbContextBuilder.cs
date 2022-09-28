using System;

namespace MockSupersets.EntityFramework.Common
{
    public interface IMockDbContextBuilder
    {
        IMockDbContextBuilder WithEntities<T>(params T[] items)
            where T : class, new();

        IMockDbContextBuilder WithEntity<T>(params Action<T>[] actions)
            where T : class, new();

        IMockDbContextBuilder WithActionOnAdd<T>(Action<T> action)
            where T : class, new();

        IMockDbContextBuilder WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new();

        IMockDbContextBuilder WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new();
    }
}