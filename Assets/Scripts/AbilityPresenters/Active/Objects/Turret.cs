using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private const float _delayBetweenShoots = 0.5f;
    private const float _rotationSpeed = 10f;
    private const float _maxShootTime = 3f;

    [SerializeField] private Transform _rotationRoot;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Rocket _rocket;

    private EnemySpawner _spawner;
    private IDamageable _nearlyEnemy;
    private Quaternion _targetRotation;

    private float _damage;
    private float _speed;
    private int _rocketCount = 1;

    private void Update()
    {
        _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Init(EnemySpawner spawner, float damage, float speed, int rocketCount, float destroyTime)
    {
        _spawner = spawner;
        _damage = damage;
        _speed = speed;
        _rocketCount = rocketCount;

        StartCoroutine(ShootProcess());
        Destroy(gameObject, destroyTime);
    }

    private IEnumerator ShootProcess()
    {
        var delay = new WaitForSeconds(_delayBetweenShoots);
        var rocketsDelay = new WaitForSeconds(_delayBetweenShoots / _rocketCount);

        var shootTime = 0f;
        while (true)
        {
            if (_nearlyEnemy == null || _nearlyEnemy.Health <= 0 || shootTime > _maxShootTime)
            {
                shootTime = 0f;
                _nearlyEnemy = _spawner.GetNearlyEnemy(transform.position);
                yield return null;
                continue;
            }

            _targetRotation = Quaternion.LookRotation(_nearlyEnemy.Root.position - _shootPoint.position, Vector3.up);
            _targetRotation.x = _targetRotation.z = 0f;
            StartCoroutine(SpawnRockets(rocketsDelay));

            yield return delay;
            shootTime += _delayBetweenShoots;
        }
    }

    private IEnumerator SpawnRockets(WaitForSeconds spanwDelay)
    {
        for (int i = 0; i < _rocketCount; i++)
        {
            var rocket = Instantiate(_rocket, _shootPoint.position, _targetRotation);
            rocket.Init(_damage, _speed, 5f);
            yield return spanwDelay;
        }
    }

    private IEnumerator LookRotation()
    {
        while (true)
        {
            if (_nearlyEnemy != null)
            {
                _targetRotation = Quaternion.LookRotation(_nearlyEnemy.Root.position - _shootPoint.position, Vector3.up);
                _targetRotation.x = _targetRotation.z = 0f;
            }

            yield return null;
        }
    }
}
