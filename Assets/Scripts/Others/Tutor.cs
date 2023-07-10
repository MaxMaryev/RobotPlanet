using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

public class Tutor : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ClickableTutor _infinity;

    private bool _completed = false;

    public event UnityAction Completed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_completed)
            return;

        _infinity.gameObject.SetActive(false);
        _completed = true;
        Completed?.Invoke();
    }
}
