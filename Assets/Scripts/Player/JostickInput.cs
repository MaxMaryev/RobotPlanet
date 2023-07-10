using UnityEngine;

public class JostickInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Joystick _joystick;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    private void Update()
    {
        if (_joystick.Direction == Vector2.zero)
        {
            _movement.Stop();
            return;
        }

        Vector3 rawDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        _movement.Move(rawDirection);
    }
}
