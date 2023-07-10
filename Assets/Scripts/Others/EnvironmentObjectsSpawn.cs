using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjectsSpawn : MonoBehaviour
{
    private const string LayerName = "EnvieromentObject";

    [SerializeField] private Player _player;
    [SerializeField] private List<GameObject> _objects = new List<GameObject>();
    [SerializeField] private float _overlapRadius = 10;
    [Header("Delay")]
    [SerializeField] private float _minDelay = 2f;
    [SerializeField] private float _maxDelay = 20f;
    [Header("Radius")]
    [SerializeField] private float _minRadius = 15f;
    [SerializeField] private float _maxRadius = 25f;

    private Timer _timer = new Timer();
    private int _layerMask;

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
        _layerMask = 1 << LayerMask.NameToLayer(LayerName);
        RestartTimer();
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    private void OnTimerComplete()
    {
        int index = Random.Range(0, _objects.Count);
        Vector3 randomPosition = _player.transform.position + RandomUtils.RandomInCirclePlane(_minRadius, _maxRadius);

        if (CanSpawn(randomPosition))
            Instantiate(_objects[index], randomPosition, Quaternion.identity);

        RestartTimer();
    }

    private bool CanSpawn(Vector3 position)
    {
        Collider[] colliders = new Collider[1];
        return Physics.OverlapSphereNonAlloc(position, _overlapRadius, colliders, _layerMask) == 0;
    }

    private void RestartTimer()
    {
        var delay = Random.Range(_minDelay, _maxDelay);
        _timer.Start(delay);
    }
}
