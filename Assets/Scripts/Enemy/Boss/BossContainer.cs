using UnityEngine;

public class BossContainer : MonoBehaviour
{
    [SerializeField] private Boss _boss;

    public Boss Boss => _boss;

    private void OnEnable()
    {
        _boss.BossDied += OnDied;
    }

    private void OnDisable()
    {
        _boss.BossDied -= OnDied;
    }

    private void OnDied(string _ = "")
    {
        Destroy(gameObject);
    }
}
