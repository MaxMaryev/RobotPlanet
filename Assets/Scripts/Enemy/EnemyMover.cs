using System.Collections;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour, IZoneMover
{
    [SerializeField] private int _maxDistanceToPlayer;
    [SerializeField] private Transform _player;

    private NavMeshAgent _agent;
    private Enemy _enemy;
    private float _previousSpeed;
    private bool _isTeleportable = true;
    private Coroutine _update;

    private void OnEnable()
    {
        _previousSpeed = _agent.speed;
        _enemy.Died += ReturnPreviousSpeed;

        _update = StartCoroutine(UpdateLoop());
    }

    private void OnDisable()
    {
        _enemy.Died -= ReturnPreviousSpeed;

        StopCoroutine(_update);
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        ReturnPreviousSpeed();
    }

    private IEnumerator UpdateLoop()
    {
        while (true)
        {
            if (_agent.isActiveAndEnabled == false)
            {
                yield return WaitForFrame(30);
                continue;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
            _agent.destination = distanceToPlayer <= 1.5f ? _player.position : _player.position - _player.forward;
            TryTeleportToOppositeSide(distanceToPlayer);
            
            yield return WaitForFrame(30);
        }
    }

    private IEnumerator WaitForFrame(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
            yield return null;
    }

    public void Init(Transform player)
    {
        _player = player;
    }

    public void ChangeSpeedZone(float modifierSpeedZone)
    {
        _agent.speed = _previousSpeed * modifierSpeedZone;
    }

    private void ReturnPreviousSpeed()
    {
        _agent.speed = _previousSpeed;
    }

    public void TryTeleportToOppositeSide(float distanceToPlayer)
    {
        if (distanceToPlayer > _maxDistanceToPlayer && _isTeleportable)
        {
            StartCoroutine(DelayingTeleport());

            Vector3 localPosition = _player.InverseTransformPoint(transform.position);
            Vector3 targetLocalPosition = -localPosition;
            Vector3 direction = targetLocalPosition - _player.position;
            transform.position = _player.TransformPoint(targetLocalPosition);
        }

        IEnumerator DelayingTeleport()
        {
            _isTeleportable = false;
            yield return new WaitForSeconds(1);
            _isTeleportable = true;
        }
    }
}
