using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour, IEnemyContainer
{
    [SerializeField] private Transform _player;
    [SerializeField] private BossContainer _boss;
    [SerializeField] private List<Transform> _enemiesContainers;
    [SerializeField] private EnemyWave _wave;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private Abilities _abilities;
    [SerializeField] private KillsCounter _killsCounter;

    private List<Enemy>[] _enemiesPools;
    private List<Enemy> _allEnemies = new List<Enemy>();
    private int _liveEnemiesCount;
    private int _numberDefeatedEnemies = 0;

    public event Action BossArrived;
    public event Action EnemyDied;

    public EnemyWave Wave => _wave;
    public int NumberDefeatedEnemies => _numberDefeatedEnemies;

    private void OnValidate()
    {
        _wave.Validate();
    }

    private void Awake()
    {
        FillEnemiesPools();
    }

    private void OnEnable()
    {
        _wave.ReadyToSpawn += OnWaveEnemyReadyToSpawn;
        _killsCounter.LimitReached += OnCreateBoss;
    }

    private void Start()
    {
        StartCoroutine(_wave.Execute());
    }

    private void OnDisable()
    {
        _wave.ReadyToSpawn -= OnWaveEnemyReadyToSpawn;

        Unsubscribe();
    }

    public IDamageable GetNearlyEnemy(Vector3 position, Transform ignoreEnemy = null)
    {
        var enabledEnemies = _allEnemies.Where(enemy => enemy.gameObject.activeSelf && enemy.Health > 0);
        IDamageable nearlyEnemy = null;
        float nearlyDistance = float.MaxValue;

        if (_boss && _boss.isActiveAndEnabled)
        {
            nearlyDistance = Vector3.SqrMagnitude(_boss.transform.position - position);
            nearlyEnemy = _boss.Boss;
        }

        foreach (var enemy in enabledEnemies)
        {
            if (enemy.transform == ignoreEnemy)
                continue;

            float distance = Vector3.SqrMagnitude(enemy.transform.position - position);

            if (distance < nearlyDistance)
            {
                nearlyEnemy = enemy;
                nearlyDistance = distance;
            }
        }

        return nearlyEnemy;
    }

    private void OnWaveEnemyReadyToSpawn(int enemyTypeIndex)
    {
        TryCreateOne(enemyTypeIndex);
    }

    private void TryCreateOne(int enemyTypeIndex)
    {
        Enemy spawningEnemy = FindFirstDisabled();

        if (spawningEnemy == null || _liveEnemiesCount >= _maxEnemies)
            return;

        spawningEnemy.transform.position = GetRandomPositionAroundPlayer();
        spawningEnemy.gameObject.SetActive(true);
        _liveEnemiesCount++;

        Enemy FindFirstDisabled()
        {
            for (int i = 0; i < _enemiesPools[enemyTypeIndex].Count; i++)
            {
                if (_enemiesPools[enemyTypeIndex][i].gameObject.activeSelf == false)
                {
                    return _enemiesPools[enemyTypeIndex][i];
                }
            }

            return null;
        }
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        var random = (min: 1, max: 360);
        int randomAngle = UnityEngine.Random.Range(random.min, random.max);

        Vector3 spawnPosition = _player.position + new Vector3(Mathf.Sin(randomAngle), 0, Mathf.Cos(randomAngle)) * _spawnDistance;
        return spawnPosition;
    }

    private void FillEnemiesPools()
    {
        _enemiesPools = new List<Enemy>[_enemiesContainers.Count];
        int enemiesTypesCount = _enemiesContainers.Count / 2;
        int miniBossesTypesCount = enemiesTypesCount;
        int maxMiniBosses = _maxEnemies / _wave.MiniBossesFrequency;

        Fill(0, enemiesTypesCount, _maxEnemies);
        Fill(enemiesTypesCount, _enemiesContainers.Count, maxMiniBosses);

        void AddEnemyToPool(int enemyTypeIndex)
        {
            var e = _wave.GetEnemyTypeByIndex(enemyTypeIndex);
            Enemy enemy = Instantiate(e, _enemiesContainers[enemyTypeIndex]);

            _enemiesPools[enemyTypeIndex].Add(enemy);
            _allEnemies.Add(enemy);

            enemy.GetComponent<EnemyMover>().Init(_player);

            if (enemy is MiniBoss)
                enemy.GetComponent<MiniBoss>().Init(_abilities);

            enemy.Died += OnReduceLiveEnemiesCount;
        }

        void Fill(int firstPool, int lastPool, int maxEnemies)
        {
            for (int pullIndex = firstPool; pullIndex < lastPool; pullIndex++)
            {
                _enemiesPools[pullIndex] = new List<Enemy>();

                for (int i = 0; i < maxEnemies; i++)
                {
                    AddEnemyToPool(pullIndex);
                }
            }
        }
    }

    private void OnCreateBoss()
    {
        _killsCounter.LimitReached -= OnCreateBoss;
        _boss.transform.position = GetRandomPositionAroundPlayer();
        _boss.gameObject.SetActive(true);

        BossArrived?.Invoke();
    }

    private void OnReduceLiveEnemiesCount()
    {
        EnemyDied?.Invoke();
        _liveEnemiesCount--;
        CountDefeatedEnemies();
    }

    private void Unsubscribe()
    {
        for (int i = 0; i < _enemiesPools.Length; i++)
        {
            foreach (var enemy in _enemiesPools[i])
            {
                enemy.Died -= OnReduceLiveEnemiesCount;
            }
        }
    }
    private void CountDefeatedEnemies()
    {
        _numberDefeatedEnemies++;
    }
}

public interface IEnemyContainer
{
    IDamageable GetNearlyEnemy(Vector3 position, Transform ignoreEnemy = null);
}
