using UnityEngine;
using UnityEngine.AI;

public class NavMeshConfigurations : MonoBehaviour
{
    [SerializeField] private float _avoidancePredictionTime;
    [SerializeField] private int _pathfindingIterationsPerFrame;

    private void Awake()
    {
        NavMesh.avoidancePredictionTime = _avoidancePredictionTime;
        NavMesh.pathfindingIterationsPerFrame = _pathfindingIterationsPerFrame;
    }

}
