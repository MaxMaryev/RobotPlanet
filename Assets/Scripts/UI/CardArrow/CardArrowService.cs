using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CardArrowService : MonoBehaviour
{
    [SerializeField] private int _maxCard = 4;
    [SerializeField] private MapAbilityContainer _abilityContainer;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private Transform _player;
    [SerializeField] private CardArrow _template;

    private List<CardArrow> _cards = new List<CardArrow>();

    private void OnEnable()
    {
        _abilityContainer.Added += OnAbilityAdded;
    }

    private void OnDisable()
    {
        _abilityContainer.Added -= OnAbilityAdded;
        _cards.ForEach(card => card.Destroyed -= OnCardDestroyed);
    }

    private void OnAbilityAdded(AbilityCardPresneter presenter)
    {
        if (_cards.Count >= _maxCard)
            return;

        var card = Instantiate(_template, _canvas);
        card.Init(presenter, _player, _canvas.localScale.x);
        card.Destroyed += OnCardDestroyed;

        _cards.Add(card);
    }

    private void OnCardDestroyed(CardArrow card)
    {
        card.Destroyed -= OnCardDestroyed;
        _cards.Remove(card);
    }
}
