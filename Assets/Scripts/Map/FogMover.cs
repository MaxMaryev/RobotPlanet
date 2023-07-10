using UnityEngine;

public class FogMover : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _player.position;
    }

    private void Update()
    {
        transform.position = _player.position + _offset;
    }
}
