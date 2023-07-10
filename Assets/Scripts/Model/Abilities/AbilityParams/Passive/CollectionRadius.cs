namespace BlobArena.Model
{
    public class CollectionRadius : AbilityParam<float>
    {
        private const string ParamName = "Collection Radius";

        public CollectionRadius(float value) : base(ParamName, value) { }
    }
}
