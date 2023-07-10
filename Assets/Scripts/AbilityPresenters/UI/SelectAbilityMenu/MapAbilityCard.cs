using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapAbilityCard : MonoBehaviour
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

    private List<AbilityModificationText> _modificationTextList = new List<AbilityModificationText>();

    public AbilityPresenter TargetAbility { get; private set; }

    public void Render(AbilityPresenter ability)
    {
        _modificationTextList.ForEach(modification => Destroy(modification.gameObject));
        _modificationTextList.Clear();

        TargetAbility = ability;
        var abilityInfo = ability.AbilityInfo;

        _nameText.text = abilityInfo.Name;
        _levelText.text = $"LV{abilityInfo.Level + 1}";
        _background.sprite = _colors.GetIconBy(abilityInfo.Type);
        _background.color = Color.white;
        _icon.sprite = _icons.GetIconBy(abilityInfo.Identifier);

        if (abilityInfo.Level == 0)
        {
            var modificationText = CreateModificationText();
            modificationText.Render(abilityInfo.Description);
            _modificationTextList.Add(modificationText);
        }
        else
        {
            foreach (var modificationInfo in abilityInfo.ModificationsInfo)
            {
                if (modificationInfo.CurrentValue == modificationInfo.NextValue)
                    continue;

                var modificationText = CreateModificationText();
                modificationText.Render($"{modificationInfo.Name}:", modificationInfo.CurrentValue, modificationInfo.UpgradeValue);

                _modificationTextList.Add(modificationText);
            }
        }
    }

    private AbilityModificationText CreateModificationText()
    {
        return Instantiate(_modificationTextTemplate, _modificationTextContainer);
    }
}
