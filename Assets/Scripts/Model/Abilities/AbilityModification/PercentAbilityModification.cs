using System;

namespace BlobArena.Model
{
    public class PercentAbilityModification : FloatAbilityModification
    {
        public PercentAbilityModification(IAbilityParam<float> target, IReadOnlyParam<float>[] parameters) : base(target, parameters) { }

        protected override string Format(float value) => $"{Math.Round(value * 100)}%";
    }
}
