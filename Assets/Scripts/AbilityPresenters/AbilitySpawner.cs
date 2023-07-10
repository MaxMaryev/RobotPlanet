using BlobArena.Model;
using UnityEngine;
using UnityEngine.Events;

public class AbilitySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [Header("Delay")]
    [SerializeField] private float _minDelay = 2f;
    [SerializeField] private float _maxDelay = 20f;
    [SerializeField] private bool _randomDelay = true;
    [SerializeField] private AnimationCurve _delayByDuration;
    [Header("Radius")]
    [SerializeField] private float _minRadius = 15f;
    [SerializeField] private float _maxRadius = 25f;
    [SerializeField] private bool _randomRadius = true;
    [SerializeField] private AnimationCurve _radiusByDuration;

    private Timer _timer = new Timer();
    private float _duration = 0f;

    public event UnityAction<Vector3> CanSpawn;

    private void OnValidate()
    {
        AnimationCurveUtils.Normalize(ref _delayByDuration);
    }

    private void OnEnable()
    {
        _timer.Completed += OnTimerComplete;
    }

    private void OnDisable()
    {
        _timer.Completed -= OnTimerComplete;
    }

    private void Start()
    {
        RestartTimer();
    }

    private void Update()
    {
        _duration += Time.deltaTime;
        _timer.Tick(Time.deltaTime);
    }

    private void OnTimerComplete()
    {
        if (_duration > _spawner.Wave.Duration)
            return;

        Vector3 random;
        if (_randomRadius)
        {
            random = RandomUtils.RandomInCirclePlane(_minRadius, _maxRadius);
        }
        else
        {
            var radius = _minRadius + _radiusByDuration.Evaluate(_duration / _spawner.Wave.Duration) * _maxRadius;
            random = RandomUtils.RandomInCirclePlane(radius, radius);
        }

        CanSpawn?.Invoke(transform.position + random);

        RestartTimer();
    }

    private void RestartTimer()
    {
        float delay;
        if (_randomDelay)
            delay = Random.Range(_minDelay, _maxDelay);
        else
            delay = _minDelay + _delayByDuration.Evaluate(_duration / _spawner.Wave.Duration) * _maxDelay;
        
        _timer.Start(delay);
    }
}
