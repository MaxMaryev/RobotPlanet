using System;
using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _damageEffect;
    [SerializeField] private ParticleSystem _explosiveEffect;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Collider _collider;

    private float _health;
    private float _maxHealt = 20;

    public event Action<float> Damaged;
    public event Action<float> HealthChanged;

    public float MaxHealth => _maxHealt;
    public float Health => _health;
    public Transform Root => transform;

    private void OnEnable()
    {
        _explosiveEffect.Stop();
        _damageEffect.Stop();       
    }

    private void Start()
    {
        _health = _maxHealt;
    }

    public void TakeDamage(float damage, string _)
    {
        _health -= damage;
        _damageEffect.Play();
        HealthChanged?.Invoke(Health);
        Damaged?.Invoke(damage);

        if (Health <= 0)
        {
            _explosiveEffect.Play();
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            StartCoroutine(DestructionObject());
        }
    }

    private IEnumerator DestructionObject()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        yield return wait;
        Destroy(gameObject);
    }

   
}
