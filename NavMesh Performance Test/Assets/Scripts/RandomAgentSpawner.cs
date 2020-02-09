using UnityEngine;

public class RandomAgentSpawner : MonoBehaviour
{
    [SerializeField]
    private RandomPathFindingMovement agentPrefab = default;
    [SerializeField]
    private Transform spawnParent = default;
    [SerializeField]
    private Collider plane = default;
    [SerializeField]
    private AgentRuntimeSet agents = default;
    [Range(0, 10000)]
    [SerializeField]
    private int spawnCount = default;

    public void SpawnAgents()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            RandomPathFindingMovement agent = Instantiate<RandomPathFindingMovement>(agentPrefab, GetRandomLocation(agentPrefab.transform.position.y), agentPrefab.transform.rotation, spawnParent);
            agent.SetPlane(plane);
        }
    }

    public void ClearAgents()
    {
        foreach (RandomPathFindingMovement agent in agents.Clear())
        {
            Destroy(agent.gameObject);
        }
    }

    private Vector3 GetRandomLocation(float y)
    {
        Vector3 location = plane.bounds.min;
        location.y = y;
        location.x += Random.value * plane.bounds.size.x;
        location.z += Random.value * plane.bounds.size.z;

        return location;
    }
}
