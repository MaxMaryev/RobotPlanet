using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityTrigger : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private List<IDamageable> _enteredEnemies = new List<IDamageable>();

    public event UnityAction<IDamageable> Entered;
    public IReadOnlyList<IDamageable> EnteredEnemies => _enteredEnemies;

    private void Awake()
    {
        _collider.isTrigger = true;
    }

    private void Update()
    {
        for (int i = _enteredEnemies.Count - 1; i >= 0; i--)
            if (_enteredEnemies[i] == null || _enteredEnemies[i].Health <= 0)
                _enteredEnemies.RemoveAt(i);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable triggered))
        {
            _enteredEnemies.Add(triggered);
            Entered?.Invoke(triggered);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IDamageable triggered))
        {
            if (_enteredEnemies.Contains(triggered))
                return;

            _enteredEnemies.Add(triggered);
            Entered?.Invoke(triggered);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable triggered))
        {
            _enteredEnemies.Remove(triggered);
        }
    }
}
