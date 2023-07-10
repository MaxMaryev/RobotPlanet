namespace BlobArena.Model
{
    public interface IAbilityParam<T> : IReadOnlyParam<T>
    {
        void SetValue(T value);
    }

    public interface IReadOnlyParam<T>
    {
        string Name { get; }
        T Value { get; }
    }
}
