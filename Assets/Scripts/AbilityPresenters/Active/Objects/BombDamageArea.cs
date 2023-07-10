using BlobArena.Model;
using UnityEngine;

public class BombDamageArea : MonoBehaviour
{
    [SerializeField] private AbilityTrigger _trigger;
    [SerializeField] private Transform _view;
    [SerializeField] private Transform _effect;

    private Timer _timer = new Timer();
    private float _damage;
    private float _cooldown;

    private void OnDestroy()
    {
        _timer.Completed -= OnTimerCompleted;
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    public void Init(float radius, float damage, float cooldown, float destroyTime)
    {
        _damage = damage;
        _cooldown = cooldown;

        _view.localScale = new Vector3(radius, radius, _view.localScale.z);
        _effect.localScale = new Vector3(radius/3, radius/3, radius/3);

        _timer.Start(_cooldown);
        _timer.Completed += OnTimerCompleted;

        Destroy(gameObject, destroyTime);
    }

    private void OnTimerCompleted()
    {
        _timer.Start(_cooldown);
        var enemies = _trigger.EnteredEnemies;

        foreach (var enemy in enemies)
        {
            if (enemy == null)
                continue;

            enemy.TakeDamage(_damage);
        }
    }
}
