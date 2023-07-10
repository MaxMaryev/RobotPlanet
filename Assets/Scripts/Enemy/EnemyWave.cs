using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class EnemyWave
{
    [SerializeField] private List<Enemy> _templates = new List<Enemy>();
    [SerializeField] private AnimationCurve _delayByDuration;
    [SerializeField] private List<AnimationCurve> _chancesOfEnemyTypeSpawn;
    [SerializeField] private int _miniBossesFrequency;
    [SerializeField] private KillsCounter _killsCounter;

    private float _currentTime;

    public event UnityAction<int> ReadyToSpawn;

    public int MiniBossesFrequency => _miniBossesFrequency;

    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float SpawnDelay { get; private set; }

    public void Validate()
    {
        Duration = Mathf.Clamp(Duration, 0f, float.MaxValue);
        SpawnDelay = Mathf.Clamp(SpawnDelay, 0f, float.MaxValue);
        AnimationCurveUtils.Normalize(ref _delayByDuration);
    }

    public Enemy GetEnemyTypeByIndex(int index)
    {
        return _templates[index];
    }

    public IEnumerator Execute()
    {
        float startTime = Time.time;
        int enemyType;

        while (true)
        {
            _currentTime = Time.time - startTime;
            enemyType = ChooseEnemyTypeForSpawn();

            ReadyToSpawn?.Invoke(enemyType);
            var delay = _delayByDuration.Evaluate(_currentTime / Duration) * SpawnDelay;
            yield return new WaitForSeconds(delay);

            if (_currentTime > Duration || _killsCounter.CurrentKills >= _killsCounter.EndGameKills)
                break;
        }
    }

    private int ChooseEnemyTypeForSpawn()
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        float probalitiesSum = 0;
        float probability = 0;
        int enemyTypeIndex;

        foreach (var chanceCurve in _chancesOfEnemyTypeSpawn)
        {
            probalitiesSum += chanceCurve.Evaluate(_currentTime / Duration);
        }

        _chancesOfEnemyTypeSpawn.OrderByDescending(x => x.Evaluate(_currentTime / Duration));

        foreach (var chanceCurve in _chancesOfEnemyTypeSpawn)
        {
            probability += chanceCurve.Evaluate(_currentTime / Duration) / probalitiesSum;

            if (probability >= random)
            {
                enemyTypeIndex = _chancesOfEnemyTypeSpawn.IndexOf(chanceCurve);
                int miniBossChance = UnityEngine.Random.Range(0, _miniBossesFrequency);

                if (miniBossChance == 0)
                    return _chancesOfEnemyTypeSpawn.IndexOf(chanceCurve) + _chancesOfEnemyTypeSpawn.Count;
                else
                    return _chancesOfEnemyTypeSpawn.IndexOf(chanceCurve);
            }
        }

        return -1;
    }
}
