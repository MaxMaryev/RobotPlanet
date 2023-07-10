using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ChangeSpeedZoneTrigger : MonoBehaviour
{
    public event UnityAction<IZoneMover> Entered;   
    public event UnityAction<IZoneMover> CameOut;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IZoneMover triggered))
        {
            Entered?.Invoke(triggered);
        }      
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IZoneMover triggered))
        {
            Entered?.Invoke(triggered);
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IZoneMover triggered))
        {
            CameOut?.Invoke(triggered);
        }      
    }
}
