using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCardCollection : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _activeCardContainer;
    [SerializeField] private Transform _passiveCardContainer;
    [SerializeField] private AbilityCardView _viewTemplate;
    [SerializeField] private GameObject _placeholderTemplate;
    [SerializeField] private Button _closeButton;

    private List<AbilityCardView> _views = new List<AbilityCardView>();
    private List<GameObject> _placeholders = new List<GameObject>();

    private void Awake()
    {
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Close);
    }

    public void Open(IEnumerable<AbilityInfo> abilityInfoList)
    {
        Time.timeScale = 0f;

        _views = new List<AbilityCardView>();
        foreach (var abilityInfo in abilityInfoList)
        {
            var container = abilityInfo.Type == AbilityType.Active ? _activeCardContainer : _passiveCardContainer;
            var view = Instantiate(_viewTemplate, container);
            view.Render(abilityInfo);

            _views.Add(view);
        }

        _placeholders = new List<GameObject>();
        var activeCount = _activeCardContainer.childCount;
        for (int i = 0; i < Abilities.MaxActiveAbilities - activeCount; i++)
        {
            var view = Instantiate(_placeholderTemplate, _activeCardContainer);
            _placeholders.Add(view);
        }

        var passiveCount = _passiveCardContainer.childCount;
        for (int i = 0; i < Abilities.MaxPassiveAbilities - passiveCount; i++)
        {
            var view = Instantiate(_placeholderTemplate, _passiveCardContainer);
            _placeholders.Add(view);
        }

        _canvas.enabled = true;
    }

    private void Close()
    {
        _views.ForEach(view => Destroy(view.gameObject));
        _views.Clear();

        _placeholders.ForEach(view => Destroy(view.gameObject));
        _placeholders.Clear();

        _canvas.enabled = false;

        Time.timeScale = 1f;
    }
}
