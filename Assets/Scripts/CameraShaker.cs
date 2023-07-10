using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _camera;

    private void OnEnable()
    {
        _player.Damaged += OnPlayerDamaged;
    }

    private void OnDisable()
    {
        _player.Damaged -= OnPlayerDamaged;
    }

    private void OnPlayerDamaged(float damage)
    {
        _camera.DOComplete(true);
        _camera.DOShakePosition(0.1f, 0.1f);
    }
}
