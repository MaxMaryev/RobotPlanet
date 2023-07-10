using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ChangeSpeedZone : MonoBehaviour
{
    [SerializeField] private float _offsetSpeed;
    [SerializeField] private float _offsetSpeedPlayer;
    [SerializeField] private ChangeSpeedZoneTrigger _zoneTrigger;

    private float _initialSpeedValue = 1f;

    public event UnityAction ZoneDestroyed;

    private void OnEnable()
    {
        transform.DOScale(12, 3);
        _zoneTrigger.Entered += SetChangeSpeed;
        _zoneTrigger.CameOut += ReturnSpeed;
    }

    private void OnDisable()
    {
        _zoneTrigger.Entered -= SetChangeSpeed;
        _zoneTrigger.CameOut -= ReturnSpeed;
    }

    private void SetChangeSpeed(IZoneMover zoneMover)
    {
        zoneMover.ChangeSpeedZone(_offsetSpeed);
    }

    private void ReturnSpeed(IZoneMover zoneMover)
    {
        zoneMover.ChangeSpeedZone(_initialSpeedValue);
    }

    public void DestroyZone()
    {
        transform.DOScale(0.1f, 3);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        WaitForSeconds wait = new WaitForSeconds(3);
        yield return wait;
        Destroy(gameObject);
    }
}
