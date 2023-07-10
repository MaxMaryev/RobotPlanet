using System;
using UnityEngine;

public interface IDamageable
{
    public event Action<float> Damaged;
    public event Action<float> HealthChanged;

    public float MaxHealth { get; }
    public float Health { get; }
    public Transform Root { get; }

    public void TakeDamage(float damage, string damageSource = "");
}
