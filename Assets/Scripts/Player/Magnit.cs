using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Magnit : MonoBehaviour
{
    [SerializeField] private Transform _moveCentr;
    [SerializeField] private float _durationMove;
    [SerializeField] private float _distance;

    private float _returnDistance;
    private Vector3 _directionOfRepulsion;

    public event UnityAction<int> Attracted;

    public void Attract(IGiver givenToPlayer)
    {
        givenToPlayer.DisableCollision();
        _directionOfRepulsion = (givenToPlayer.transform.position - _moveCentr.position).normalized;
        Vector3 EndPointOfRebound = _directionOfRepulsion * _distance + givenToPlayer.transform.position;
        Vector3 RiseOfExperience = new Vector3(0, 1, 0);
        givenToPlayer.transform.DOMove(EndPointOfRebound + RiseOfExperience, _durationMove).OnComplete(() =>
          {
              givenToPlayer.transform.SetParent(_moveCentr);
              Vector3 endPoint = new Vector3(0, 0.5f, 0);
              _returnDistance = Mathf.Sqrt(Mathf.Pow((givenToPlayer.transform.localPosition.x - endPoint.x), 2)
              + Mathf.Pow((givenToPlayer.transform.localPosition.y - endPoint.y), 2)
              + Mathf.Pow((givenToPlayer.transform.localPosition.z - endPoint.z), 2));
              givenToPlayer.transform.DOLocalMove(endPoint, (_returnDistance / _distance) * _durationMove).OnComplete(() =>
               {
                   Attracted?.Invoke(givenToPlayer.Points);
                   givenToPlayer.Take();
               }
              );
          });
    }
}
