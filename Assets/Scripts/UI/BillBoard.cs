using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private bool _xRotate = true;

    private Transform _camera;
    private Quaternion _rotation;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.forward = _camera.forward;

        if (_xRotate == false)
        {
            _rotation = transform.localRotation;
            transform.localRotation = Quaternion.Euler(0, _rotation.eulerAngles.y, _rotation.eulerAngles.z);
        }
    }
}
