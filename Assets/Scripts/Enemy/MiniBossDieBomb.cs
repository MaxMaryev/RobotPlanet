using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MiniBossDieBomb : MonoBehaviour
{
    [SerializeField] private int _radiusDefeat;
    [SerializeField] private float _damage;
    [SerializeField] ParticleSystem _explosiveEffect;
    [SerializeField] SkinnedMeshRenderer _meshRenderer;

    private void OnEnable()
    {
        _explosiveEffect.Stop();
        StartCoroutine(Delay());
        _meshRenderer.material.DOColor(Color.red, 0.1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void DamageOthers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radiusDefeat);
        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
        StartCoroutine(DestructionObject());
    }

    private IEnumerator Delay()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        yield return wait;
        DamageOthers();
        _explosiveEffect.Play();
        _meshRenderer.enabled = false;
    }

    private IEnumerator DestructionObject()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        yield return wait;
        Destroy(gameObject);
    }
}
