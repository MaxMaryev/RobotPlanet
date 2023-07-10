using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private int _distanceBeforeMove;

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        float distanceFromCenterAxisX = Mathf.Abs(_player.position.x - transform.position.x);
        float distanceFromCenterAxisZ = Mathf.Abs(_player.position.z - transform.position.z);

        if (distanceFromCenterAxisX >= _distanceBeforeMove)
            transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
        else if (distanceFromCenterAxisZ >= _distanceBeforeMove)
            transform.position = new Vector3(transform.position.x, transform.position.y, _player.position.z);
    }
}
