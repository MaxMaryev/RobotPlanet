using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;

public class AbilitiesView : MonoBehaviour
{
    [SerializeField] private List<AbilityView> _receivedActiveAbilities;
    [SerializeField] private List<AbilityView> _receivedPassiveAbilities;
    [SerializeField] private AbilityTypeColors _colors;

    private void Awake()
    {
        Render(_receivedActiveAbilities, AbilityType.Active);
        Render(_receivedPassiveAbilities, AbilityType.Passive);
    }

    public void Add(AbilityPresenter ability)
    {
        List<AbilityView> abilities = ability.AbilityInfo.Type == AbilityType.Active ? 
                                                _receivedActiveAbilities : _receivedPassiveAbilities;

        abilities.First(view => view.Initialized == false).Init(ability);
    }

    private void Render(List<AbilityView> views, AbilityType type)
    {
        foreach (var view in views)
            view.Render(_colors.GetIconBy(type));
    }
}
