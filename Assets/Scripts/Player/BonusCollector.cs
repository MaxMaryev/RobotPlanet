using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BonusCollector : MonoBehaviour
{
    [SerializeField] private Magnit _bonusMagnit;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _bonusMagnit.Attracted += OnBonusAttracted;
    }

    private void OnDisable()
    {
        _bonusMagnit.Attracted -= OnBonusAttracted;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out BonusHealt bonus))
        {
            _bonusMagnit.Attract(bonus);
        }
    }

    private void OnBonusAttracted(int bonus)
    {
        _player.SetCurrentHealthModifier(bonus);
    }
}
