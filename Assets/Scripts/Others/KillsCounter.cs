using System;
using UnityEngine;

public class KillsCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    public event Action LimitReached;
    public event Action Updated;

    public int EndGameKills { get; private set; }
    public int CurrentKills { get; private set; }

    private void Awake()
    {
        EndGameKills = Mathf.RoundToInt(_spawner.Wave.Duration / _spawner.Wave.SpawnDelay);
    }

    private void OnEnable()
    {
        _spawner.EnemyDied += CountKills;
    }

    private void OnDisable()
    {
        _spawner.EnemyDied -= CountKills;
    }

    private void CountKills()
    {
        CurrentKills++;
        Updated?.Invoke();

        if (CurrentKills == EndGameKills)
            LimitReached?.Invoke();
    }
}
