using System;
using UnityEngine;

public class MiniBoss : Enemy
{
    [SerializeField] private MiniBossDieBomb _dieBomb;
    private Abilities _abilities;

    public new event Action Died;

    public void Init(Abilities abilities)
    {
        _abilities = abilities;
    }

    protected override void Die()
    {
        base.Die();       
        Instantiate(_dieBomb, transform.position, Quaternion.identity);
    }
}
