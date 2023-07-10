using System;

namespace BlobArena.Model
{
    public class IntAbilityModification : AbilityModification<int>
    {
        public IntAbilityModification(IAbilityParam<int> target, IReadOnlyParam<int>[] parameters) : base (target, parameters) { }


        protected override int CalculateUpgradeValue(int currentValue, int nextValue) => nextValue - currentValue;
        protected override string Format(int value) => value.ToString();
        protected override int Abs(int value) => Math.Abs(value);
        protected override string Symbol(int upgradeValue) => upgradeValue > 0 ? "+" : "-";
    }
}
