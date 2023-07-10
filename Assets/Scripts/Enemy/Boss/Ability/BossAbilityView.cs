using UnityEngine;

public class BossAbilityView : MonoBehaviour
{
    [SerializeField] private BossDamageArea[] _damageAreas;
    [SerializeField] private GameObject _particalSystem;

    private void OnEnable()
    {
        foreach (var area in _damageAreas)
        {
            area.AttackComplete += OnShot;
        }
    }

    private void OnDisable()
    {
        foreach (var area in _damageAreas)
        {
            area.AttackComplete -= OnShot;
        }
    }

    private void OnShot(BossDamageArea damageArea)
    {
        var particleSystem = Instantiate(_particalSystem);
        particleSystem.transform.position = damageArea.transform.position;
    }
}
