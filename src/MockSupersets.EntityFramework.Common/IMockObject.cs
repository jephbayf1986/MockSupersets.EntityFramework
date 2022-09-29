namespace MockSupersets.EntityFramework.Common
{
    public interface IMockObject<TObject>
    {
        TObject Object { get; }
    }
}