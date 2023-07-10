using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketDrone : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private float _flyDistance;

    private NavMeshAgent _agent;
    private Vector3 _destination;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();    
    }

    private void Start()
    {
        StartCoroutine(Patrol());    
    }


    public void Enable()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            _destination = GetRandomPointAroundPlayer();
            _agent.destination = _destination;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, _destination) < 0.1f || Vector3.Distance(_player.transform.position, transform.position) > 3);
        }
    }

    private Vector3 GetRandomPointAroundPlayer()
    {
        var random = (min: -_flyDistance, max: _flyDistance);
        Vector3 randomShift = new Vector3(Random.Range(random.min, random.max), transform.position.y, Random.Range(random.min, random.max));

        Vector3 playerVelocity = _player.GetAgentVelocity();
        return _player.transform.position + randomShift;
    }
}
