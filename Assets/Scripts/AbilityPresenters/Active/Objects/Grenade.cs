using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private AbilityTrigger _modelTrigger;
    [SerializeField] private AbilityTrigger _explosionTrigger;
    [SerializeField] private ParticleSystem _effect;

    private float _damage;
    private float _speed;

    private void OnEnable()
    {
        _modelTrigger.Entered += OnEntered;
    }

    private void OnDisable()
    {
        _modelTrigger.Entered -= OnEntered;
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        Rotate();
    }

    public void Init(float damage, float speed, float radius, float destroyTime)
    {
        _damage = damage;
        _speed = speed;
        _explosionTrigger.transform.localScale = Vector3.one * radius;

        Destroy(gameObject, destroyTime);
    }

    private void OnEntered(IDamageable _)
    {
        _effect.Play();

        var enemies = _explosionTrigger.EnteredEnemies;

        foreach (var enemy in enemies)
            enemy.TakeDamage(_damage);

        _speed = 0;
        _modelTrigger.gameObject.SetActive(false);
        Destroy(gameObject, _effect.main.duration);
    }

    private void Rotate()
    {
        float rotationSpeed = 2;
        _modelTrigger.transform.Rotate(Vector3.right + Vector3.up, rotationSpeed);
    }
}
