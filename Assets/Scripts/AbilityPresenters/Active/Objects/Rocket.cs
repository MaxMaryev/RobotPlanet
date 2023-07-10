using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float _speed;
    private float _damage;
    private int _maxPassThroughCount;
    private int _currentPassThroughCount;

    public event Action Collided;

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable enemy))
        {
            _currentPassThroughCount += 1;
            enemy.TakeDamage(_damage);

            if (_currentPassThroughCount >= _maxPassThroughCount)
                Destroy(gameObject);
        }

        Collided?.Invoke();
    }

    public void Init(float damage, float speed, float destroyTime, int passThroighCount = 1)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        Destroy(gameObject, destroyTime);

        _speed = speed;
        _damage = damage;
        _maxPassThroughCount = passThroighCount;
    }
}
