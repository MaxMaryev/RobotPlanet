using System.Collections;
using UnityEngine;

public class EnemyView : MonoBehaviour
{

    private Enemy _enemy;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Collider _collider;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        _meshRenderer.enabled = true;
        _bloodEffect.Stop();
        _enemy.Died += OnDie;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnDie;
    }

    private void OnDie()
    {
        StartCoroutine(Die());

        IEnumerator Die()
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _bloodEffect.Play();
            yield return new WaitForSeconds(_bloodEffect.main.duration);

            gameObject.SetActive(false);
        }
    }
}
