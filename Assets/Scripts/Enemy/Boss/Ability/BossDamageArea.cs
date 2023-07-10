using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BossDamageArea : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private BossAbility _bossAbility;
    [SerializeField] private float _damage;

    private SphereCollider _collider;

    public event Action BeginAttack;
    public event Action<BossDamageArea> AttackComplete;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();    
    }

    private void OnEnable()
    {
        BeginAttack?.Invoke();
        SetTargetArea();
        _collider.enabled = false;
        StartCoroutine(EnableColliderForAttack());    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage, nameof(BossDamageArea));
        }
    }

    private void SetTargetArea()
    {
        var random = (min: -3f, max: 3f);
        Vector3 randomShift = new Vector3(UnityEngine.Random.Range(random.min, random.max), 0, UnityEngine.Random.Range(random.min, random.max));

        Vector3 playerVelocity = _player.GetAgentVelocity();
        transform.position = _player.transform.position + playerVelocity + randomShift;
    }

    private IEnumerator EnableColliderForAttack()
    {
        float timeOffset = 0.1f;

        yield return new WaitForSeconds(_bossAbility.Duration - timeOffset);
        AttackComplete?.Invoke(this);

        _collider.enabled = true;
    }
}
