using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface plane = default;

    private void Start()
    {
        Build();
    }

    public void Build()
    {
        plane.BuildNavMesh();
    }
}
