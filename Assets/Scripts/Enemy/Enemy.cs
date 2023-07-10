using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;

    private float _health;
    private Coroutine _attack;
    private WaitForSeconds _waitAttack;

    public event Action Died;

    public event Action<float> Damaged;
    public event Action<float> HealthChanged;


    [field: SerializeField] public float MaxHealth { get; private set; }

    public float Health => _health;
    public Transform Root => transform;

    private void Awake()
    {
        _waitAttack = new WaitForSeconds(_attackSpeed);
    }

    private void OnEnable()
    {
        _health = MaxHealth;
    }

    private void OnDisable()
    {
        if (_attack != null)
            StopCoroutine(_attack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            _attack = StartCoroutine(Attack(player));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (_attack != null)
                StopCoroutine(_attack);
        }
    }

    public void TakeDamage(float damage, string _ = "")
    {
        _health -= damage;
        HealthChanged?.Invoke(Health);
        Damaged?.Invoke(damage);

        if (Health <= 0)
            Die();
    }

    private IEnumerator Attack(Player player)
    {
        while (Health > 0 && player.Health > 0)
        {
            player.TakeDamage(_damage, nameof(Enemy));
            yield return _waitAttack;
        }
    }

    protected virtual void Die()
    {
        Died?.Invoke();
    }
}
