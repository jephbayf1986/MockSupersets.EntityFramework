using System;

namespace MockSupersets.EntityFramework.Common
{
    public interface IMockDbContextBuilder<TMock>
    {
        TMock WithEntities<T>(params T[] items)
            where T : class, new();

        TMock WithEntity<T>(params Action<T>[] actions)
            where T : class, new();

        TMock WithActionOnAdd<T>(Action<T> action)
            where T : class, new();

        TMock WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new();

        TMock WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new();
    }
}