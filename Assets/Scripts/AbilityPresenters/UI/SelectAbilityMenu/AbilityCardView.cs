using BlobArena.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCardView : MonoBehaviour
{
    [SerializeField] private Transform _modificationTextContainer;
    [SerializeField] private AbilityModificationText _modificationTextTemplate;
    [SerializeField] private AbilityTypeColors _colors;
    [SerializeField] private AbilityIcons _icons;
    [Space(10f)]
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;

    public void Render(AbilityInfo abilityInfo)
    {
        if (abilityInfo.Modification.CurrentLevel == abilityInfo.Modification.MaxLevel)
            _levelText.text = $"MAX";
        else
            _levelText.text = $"LV{abilityInfo.Level}";

        _nameText.text = abilityInfo.Name;
        _background.sprite = _colors.GetIconBy(abilityInfo.Type);
        _background.color = Color.white;
        _icon.sprite = _icons.GetIconBy(abilityInfo.Identifier);

        var instModificationText = CreateModificationText();
        instModificationText.Render(abilityInfo.Description);
    }

    private AbilityModificationText CreateModificationText()
    {
        return Instantiate(_modificationTextTemplate, _modificationTextContainer);
    }
}
