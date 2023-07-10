using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class MeshVisibleEvents : MonoBehaviour
{
    public event UnityAction BecameVisible;
    public event UnityAction BecameInvisible;

    private void OnBecameVisible()
    {
        BecameVisible?.Invoke();
    }

    private void OnBecameInvisible()
    {
        BecameInvisible?.Invoke();
    }
}
