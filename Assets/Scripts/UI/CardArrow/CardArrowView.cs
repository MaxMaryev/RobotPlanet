using BlobArena.Model;
using UnityEngine;
using UnityEngine.UI;

public class CardArrowView : MonoBehaviour
{
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private Image[] _colorImages;
    [SerializeField] private AbilityTypeColors _colors;
    [SerializeField] private AbilityIcons _icons;


    public void Render(AbilityInfo info)
    {
        _abilityIcon.sprite = _icons.GetIconBy(info.Identifier);

        var color = _colors.GetColorBy(info.Type);
        foreach (var image in _colorImages)
            image.color = color;
    }
}
