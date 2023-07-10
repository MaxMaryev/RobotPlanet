using UnityEngine;
using DG.Tweening;
using BlobArena.Model;

public class SpikesFromGround : MonoBehaviour
{
    [SerializeField] private AbilityTrigger _trigger;
    [SerializeField] private Transform _view;
    [SerializeField] private ParticleSystem _growingEffect;

    private Timer _timer = new Timer();
    private float _damage;
    private float _cooldown;

    private void OnEnable()
    {
        _growingEffect.Play();
        transform.DOMoveY(0f, 1f).OnComplete(() =>
        {
            transform.DOMoveY(-2f, 1f);
        });
    }

    private void OnDestroy()
    {
        _timer.Completed -= OnTimerCompleted;
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    public void Init(/*float radius,*/ float damage, float cooldown, float destroyTime)
    {
        _damage = damage;
        _cooldown = cooldown;

       
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
