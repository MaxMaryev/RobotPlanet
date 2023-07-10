using System;
using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;

[CreateAssetMenu(fileName = "New Types Colors", menuName = "Ability/Colors", order = 66)]
public class AbilityTypeColors : ScriptableObject
{
    [SerializeField] private List<TypeColor> _colors;

    public Color GetColorBy(AbilityType type)
    {
        return _colors.Find(color => color.Type == type).Color;
    }

    public Sprite GetIconBy(AbilityType type)
    {
        return _colors.Find(color => color.Type == type).Icon;
    }
    
    [Serializable]
    private class TypeColor
    {
        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
