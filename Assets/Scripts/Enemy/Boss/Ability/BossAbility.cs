using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class BossAbility : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BossDamageArea[] _damageAreas;
    [SerializeField] private float _delayBetweenAttacks;

    private Boss _boss;

    [field: SerializeField] public float Duration { get; private set; }

    private void Awake()
    {
        _boss = GetComponent<Boss>();
    }

    public IEnumerator Use()
    {
        WaitForSeconds delayBetweenAttacks = new WaitForSeconds(_delayBetweenAttacks);
        WaitForSeconds waitAttack = new WaitForSeconds(Duration);

        while (_boss.Health > 0 || _player.Health > 0)
        {
            yield return delayBetweenAttacks;

            foreach (var damageArea in _damageAreas)
            {
                damageArea.gameObject.SetActive(true);
            }

            yield return waitAttack;

            foreach (var damageArea in _damageAreas)
            {
                damageArea.gameObject.SetActive(false);
            }
        }
    }
}
