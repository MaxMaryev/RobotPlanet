namespace BlobArena.Model
{
    public class ProjectileSpeed : AbilityParam<float>
    {
        private const string ParamName = "Projectile Speed";

        public ProjectileSpeed(float value) : base(ParamName, value) { }
    }
}
