using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CristallChangeSpeedZone : MonoBehaviour
{
    [SerializeField] private ChangeSpeedZone _decelerationAcelerationZone;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Texture _texturePurple;
    [SerializeField] private Texture _textureGray;
    [SerializeField] private float _zoneLifetime;

    private bool _isZoneSpawn = false;
    public event UnityAction<CristallChangeSpeedZone> Destroed;

    private void OnEnable()
    {       
        StartCoroutine(DestroyCristall());
        _decelerationAcelerationZone.ZoneDestroyed += MakeCrystalColorInitial;
    }

    private void OnDisable()
    {
        _decelerationAcelerationZone.ZoneDestroyed -= MakeCrystalColorInitial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_isZoneSpawn == false)
            {
                ZoneSpawn();
                _isZoneSpawn = true;
            }
        }
    }

    private void ZoneSpawn()
    {
        ChangeSpeedZone activZone = Instantiate(_decelerationAcelerationZone, transform.position, Quaternion.identity);
        _meshRenderer.material.mainTexture = _texturePurple;
        StartCoroutine(DestroyZone(activZone));
    }

    private IEnumerator DestroyZone(ChangeSpeedZone changeSpeedZone)
    {
        WaitForSeconds wait = new WaitForSeconds(_zoneLifetime);
        yield return wait;
        changeSpeedZone.DestroyZone();
        _isZoneSpawn = false;
        MakeCrystalColorInitial();
    }

    private IEnumerator DestroyCristall()
    {
        WaitForSeconds wait = new WaitForSeconds(120);
        yield return wait;
        if (_isZoneSpawn == false)
        {
            Destroy(gameObject);
            Destroed?.Invoke(this);
        }
        else
        {
            StartCoroutine(DestroyCristall());
        }
    }

    private void MakeCrystalColorInitial()
    {
        _meshRenderer.material.mainTexture = _textureGray;
    }
}
