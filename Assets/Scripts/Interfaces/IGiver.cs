using UnityEngine;

public interface IGiver
{
    [SerializeField] public int Points { get; }
   
    public Transform transform { get; }

    public void DisableCollision();
    public void Take();
}
