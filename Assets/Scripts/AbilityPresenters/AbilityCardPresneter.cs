using System.Collections;
using UnityEngine;
using BlobArena.Model;
using UnityEngine.Events;

public class AbilityCardPresneter : MonoBehaviour
{
    public const float DestroySqrtDistance = 2000;

    [SerializeField] private MapAbilityCard _card;
    [SerializeField] private CollectCardView _view;
    [SerializeField] private ParticleSystem _destroyEffect;

    private Abilities _abilities;
    private Transform _player;

    public event UnityAction<AbilityCardPresneter> Collected;
    public event UnityAction<AbilityCardPresneter> Destroyed;

    public AbilityPresenter Ability => _card.TargetAbility;

    private void OnDisable()
    {
        if (_abilities)
        {
            _abilities.Collected -= OnAbilityCollected;
            _abilities.Upgraded -= OnAbilityUpgraded;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Abilities abilities))
        {
            _abilities.Collected -= OnAbilityCollected;
            _abilities.Upgraded -= OnAbilityUpgraded;

            abilities.AddAbility(Ability);
            Collected?.Invoke(this);

            _view.ShowCard(() => DestroyWithEffect());
        }
    }

    public void Init(Abilities abilities, AbilityPresenter presenter, Transform player)
    {
        _abilities = abilities;
        _card.Render(presenter);
        _player = player;

        _abilities.Collected += OnAbilityCollected;
        _abilities.Upgraded += OnAbilityUpgraded;

        StartCoroutine(TryDestroy());
    }

    private void OnAbilityCollected(AbilityPresenter ability)
    {
        if (ability.Equals(Ability) && Ability.CanUpgrade == false)
            DestroyWithEffect();

        if (_abilities.Contains(Ability))
            return;

        if (Ability.AbilityInfo.Type == AbilityType.Active && _abilities.ActiveCount == Abilities.MaxActiveAbilities ||
            Ability.AbilityInfo.Type == AbilityType.Passive && _abilities.PassiveCount == Abilities.MaxPassiveAbilities)
            DestroyWithEffect();
    }

    private void OnAbilityUpgraded(AbilityPresenter ability)
    {
        if (ability.Equals(Ability))
            _card.Render(Ability);
    }

    private IEnumerator TryDestroy()
    {
        var delay = new WaitForSeconds(5f);
        while (true)
        {
            yield return delay;

            if (Vector3.SqrMagnitude(_player.position - transform.position) > DestroySqrtDistance)
                DestroyWithEffect();
        }
    }

    private void DestroyWithEffect()
    {
        Instantiate(_destroyEffect, transform.position + Vector3.up, _destroyEffect.transform.rotation);

        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
