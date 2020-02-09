using UnityEngine;
using UnityEngine.AI;

public class ClickAgentMover : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask = default;
    [SerializeField]
    private Camera cam = default;
    [SerializeField]
    private NavMeshAgent leftClickAgent = default;
    [SerializeField]
    private NavMeshAgent rightClickAgent = default;

    void Update()
    {
        if (leftClickAgent != null && Input.GetMouseButtonDown(0))
        {
            MoveAgentToMousePosition(leftClickAgent);
        }
        
        if (rightClickAgent != null && Input.GetMouseButtonDown(1))
        {
            MoveAgentToMousePosition(rightClickAgent);
        }
    }

    private void MoveAgentToMousePosition(NavMeshAgent agent)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 destination = hit.point;
            agent.SetDestination(destination);
        }
    }
}
