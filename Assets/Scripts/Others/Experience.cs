using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public class Experience : MonoBehaviour, IGiver
{
    private Collider _collider;

    [field: SerializeField] public int Points { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        transform.DORotate(new Vector3(0, 360, 0), 1.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    public void Take()
    {
        Destroy(gameObject);
    }

    public void DisableCollision()
    {
        _collider.enabled = false;
    }
}
