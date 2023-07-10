using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private BossAbility _ability;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _meleeAttackSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _deadEffect;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _mesh;

    private float _currentHealth;
    private Coroutine _meleeAttack;
    private WaitForSeconds _waitMeleeAttack;

    public event Action<float> Damaged;
    public event Action<float> HealthChanged;
    public event Action<string> BossDied;

    public float Health => _currentHealth;
    public float MaxHealth => _maxHealth;
    public Transform Root => transform;

    private void Awake()
    {
        _deadEffect.Stop();
        _currentHealth = MaxHealth;
        _waitMeleeAttack = new WaitForSeconds(_meleeAttackSpeed);
    }

    private void Start()
    {
        StartCoroutine(_ability.Use());
    }

    private void OnDisable()
    {
        if (_meleeAttack != null)
            StopCoroutine(_meleeAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            _meleeAttack = StartCoroutine(AttackWithMelee(player));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (_meleeAttack != null)
                StopCoroutine(_meleeAttack);
        }
    }

    public void TakeDamage(float damage, string damageSource = "")
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(Health);
        Damaged?.Invoke(damage);

        if (Health <= 0)
        {
            _canvas.SetActive(false);
            _mesh.SetActive(false);
            _deadEffect.Play();
            StartCoroutine(EffectDied(damageSource));
        }
    }

    private IEnumerator AttackWithMelee(Player player)
    {
        while (Health > 0 & player.Health > 0)
        {
            _animator.SetBool("Attack", true);
            player.TakeDamage(_meleeDamage, nameof(Boss));
            yield return _waitMeleeAttack;
            _animator.SetBool("Attack", false);
        }
    }

    private IEnumerator EffectDied(string damageSource = "")
    {
        WaitForSeconds DelayEffect = new WaitForSeconds(2f);
        yield return DelayEffect;
        BossDied?.Invoke(damageSource);
    }
}
