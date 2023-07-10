using System;
using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;

[CreateAssetMenu(fileName = "New Types Colors", menuName = "Ability/Icons", order = 66)]
public class AbilityIcons : ScriptableObject
{
    [SerializeField] private List<AbilityIcon> _icons;

    public Sprite GetIconBy(AbilityIdentifier identifier)
    {
        return _icons.Find(icon => icon.Identifier == identifier).Icon;
    }

    [Serializable]
    private class AbilityIcon
    {
        [field: SerializeField] public AbilityIdentifier Identifier { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
