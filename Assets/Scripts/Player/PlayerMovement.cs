using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;

    private NavMeshAgent _agent;
    private float _speedModifier = 1f;
    private float _speedModifierZone = 1f;
    private bool _isPlayerDied = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 direction)
    {
        if (_isPlayerDied == false)
        {
            _model.LookAt(_model.position + direction);
            _agent.velocity = direction * _speed * _speedModifier * _speedModifierZone;
            _animator.SetFloat("Speed", direction.magnitude);
        }
    }

    public void Stop()
    {
        _animator.SetFloat("Speed", 0);
    }

    public void SetSpeedModifier(float modifier)
    {
        if (modifier < 1f)
            throw new ArgumentOutOfRangeException(nameof(modifier));

        _speedModifier = modifier;
    }

    public Vector3 GetAgentVelocity()
    {
        return _agent.velocity;
    }

    public void StopPlayerDied()
    {
        _isPlayerDied = true;
    }
}
