namespace BlobArena.Model
{
    public class AbilityParam<T> : IAbilityParam<T>
    {
        public AbilityParam(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public T Value { get; private set; }

        public void SetValue(T value)
        {
            Value = value;
        }
    }
}
