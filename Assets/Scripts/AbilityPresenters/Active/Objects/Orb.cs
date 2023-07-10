using System;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private float _damage;

    public void SetDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
}
