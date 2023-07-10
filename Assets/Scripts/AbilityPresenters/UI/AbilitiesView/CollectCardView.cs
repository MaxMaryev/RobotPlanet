using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CollectCardView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _distanceToCamera;
    [SerializeField] private float _topShift;

    private Coroutine _showCard;

    public void ShowCard(UnityAction onShown)
    {
        if (_showCard != null)
            StopCoroutine(_showCard);

        _showCard = StartCoroutine(ShowCardWithDelay(onShown));
    }

    private IEnumerator ShowCardWithDelay(UnityAction onShown)
    {
        var showTime = 2f;
        var camera = Camera.main;
        StartCoroutine(MoveRotation(camera, showTime));
        StartCoroutine(MovePosition(camera, showTime));

        yield return new WaitForSeconds(showTime);

        _canvasGroup.DOFade(0f, 0.6f).OnComplete(() => onShown?.Invoke());
    }

    private IEnumerator MoveRotation(Camera camera, float delay)
    {
        var startTime = Time.time;
        while (Time.time - startTime < delay)
        {
            _canvas.transform.rotation = Quaternion.Lerp(_canvas.transform.rotation, camera.transform.rotation, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MovePosition(Camera camera, float delay)
    {
        var startTime = Time.time;
        Vector3 targetPosition;
        while (Time.time - startTime < delay)
        {
            targetPosition = camera.transform.position + camera.transform.forward * _distanceToCamera + camera.transform.up * _topShift;
            _canvas.transform.position = Vector3.Lerp(_canvas.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            _moveSpeed += Time.deltaTime * 10;
            yield return null;
        }
    }
}
