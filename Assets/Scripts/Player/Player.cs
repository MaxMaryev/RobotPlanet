using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private ParticleSystem _damageEffect;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderers;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _currentHealth;
    private float _maxHealthModifier = 1f;
    private float _damageReduction = 1f;
    private bool _isDeaded = false;

    public event Action<float> Damaged;
    public event Action<float> HealthChanged;
    public event Action<string> PlayerDied;
    public event Action DecreasedHalf;

    public float MaxHealth => _maxHealth * _maxHealthModifier;
    public float Health => _currentHealth;
    public Transform Root => transform;

    private void OnEnable()
    {
        _damageEffect.Stop();
        _deathEffect.Stop();
    }

    private void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage, string damageSource = "")
    {
        if (_isDeaded == false)
        {
            Damaged?.Invoke(damage);
            _damageEffect.Play();
            _currentHealth -= damage * _damageReduction;
            HealthChanged?.Invoke(_currentHealth);
            if (_currentHealth <= _maxHealth / 2)
            {
                DecreasedHalf?.Invoke();
            }
            if (_currentHealth <= 0)
            {
                foreach (var mech in _skinnedMeshRenderers)
                {
                    mech.enabled = false;
                }
                _deathEffect.Play();
                _playerMovement.StopPlayerDied();
                StartCoroutine(Destroy(damageSource));
                _isDeaded = true;
            }
        }
    }

    public void SetHealthModifier(float modifier)
    {
        if (modifier < 1f)
            throw new ArgumentOutOfRangeException(nameof(modifier));

        _maxHealthModifier = modifier;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void SetDamageReduction(float modifier)
    {
        if (modifier <= 0)
            throw new ArgumentOutOfRangeException(nameof(modifier));

        _damageReduction = modifier;
    }

    public float Regenerate(float modifier)
    {
        var regeneratedHealth = Mathf.Clamp(MaxHealth * modifier, 0, MaxHealth - _currentHealth);
        _currentHealth += regeneratedHealth;
        HealthChanged?.Invoke(_currentHealth);
        return regeneratedHealth;
    }

    public void SetCurrentHealthModifier(float modifier)
    {
        if (modifier < 1f)
            throw new ArgumentOutOfRangeException(nameof(modifier));

        _currentHealth += modifier;
        HealthChanged?.Invoke(_currentHealth);
    }

    private IEnumerator Destroy(string damageSource)
    {
        WaitForSeconds wait = new WaitForSeconds(1.5f);
        yield return wait;
        PlayerDied?.Invoke(damageSource);
    }
}