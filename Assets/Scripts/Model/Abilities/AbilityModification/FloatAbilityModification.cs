using System;

namespace BlobArena.Model
{
    public class FloatAbilityModification : AbilityModification<float>
    {
        public FloatAbilityModification(IAbilityParam<float> target, IReadOnlyParam<float>[] parameters) : base(target, parameters) { }

        protected override float CalculateUpgradeValue(float currentValue, float nextValue) => nextValue - currentValue;
        protected override string Format(float value) => value.ToString("0.##");
        protected override float Abs(float value) => Math.Abs(value);
        protected override string Symbol(float upgradeValue) => upgradeValue > 0 ? "+" : "-";
    }
}
