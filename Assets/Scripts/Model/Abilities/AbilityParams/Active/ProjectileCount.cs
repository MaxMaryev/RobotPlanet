namespace BlobArena.Model
{
    public class ProjectileCount : AbilityParam<int>
    {
        private const string ParamName = "Projectile Count";

        public ProjectileCount(int value) : base(ParamName, value) { }
    }
}
