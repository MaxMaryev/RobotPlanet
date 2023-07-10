using System;
using UnityEngine;
using DG.Tweening;

public class Lightning : MonoBehaviour
{
    private IEnemyContainer _enemyContainer;
    private float _speed;
    private float _damage;
    private int _maxJumpCount;
    private int _currentJumpCount;

    public void Init(IEnemyContainer enemyContainer, float speed, float damage, int jumpCount)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _speed = speed;
        _damage = damage;
        _maxJumpCount = jumpCount;
        _enemyContainer = enemyContainer;

        MoveTo(_enemyContainer.GetNearlyEnemy(transform.position).Root);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_damage);
    }

    private void MoveTo(Transform target)
    {
        target.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.DOMove(target.transform.position, Vector3.Distance(target.position, transform.position) / _speed)
            .OnComplete(() => 
        {
            _currentJumpCount += 1;

            if (_currentJumpCount >= _maxJumpCount)
                Destroy(gameObject);
            else
                MoveTo(_enemyContainer.GetNearlyEnemy(transform.position, target).Root);
        });
    }
}
