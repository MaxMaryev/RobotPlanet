using UnityEngine;
using DG.Tweening;
using System;

public class FallingBomb : MonoBehaviour
{
    [SerializeField] private AbilityTrigger _trigger;

    private event Action _onFall;

    public FallingBomb Init(Vector3 fallTarget, float fallDamage, float fallDuration)
    {
        transform.LookAt(fallTarget);
        transform.DOMove(fallTarget, fallDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            var enemies = _trigger.EnteredEnemies;

            foreach (var enemy in enemies)
            {
                if (enemy == null)
                    continue;

                enemy.TakeDamage(fallDamage);
            }

            _onFall?.Invoke();
            _onFall = null;

            Destroy(gameObject);
        });

        return this;
    }

    public void OnFall(Action action)
    {
        _onFall = action;
    }
}
