using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Renderer))]
public class RandomPathFindingMovement : MonoBehaviour
{
    [SerializeField]
    private AgentRuntimeSet agents = default;

    [Tooltip("The plane that the creatures can wander on")]
    [SerializeField]
    private Collider plane = default;

    [Tooltip("The material that the agent has when it has a path")]
    [SerializeField]
    private Material activeMaterial = default;

    [Tooltip("The material that the agent has when it is searching for a path")]
    [SerializeField]
    private Material inactiveMaterial = default;

    [Tooltip("The min amount of seconds before a new destination is chosen (new path calculation)")]
    [SerializeField]
    private float minPathFindPeriod = 2;

    [Tooltip("The max amount of seconds before a new destination is chosen (new path calculation)")]
    [SerializeField]
    private float maxPathFindPeriod = 2;

    [Tooltip("The max distance for the generated destination")]
    [SerializeField]
    private float range = 50;

    private NavMeshAgent agent;
    private Renderer rend;

    private float timeUntilNextPath = 0;
    private bool searchingForPath = false;
    private float currentSearchTime = 0;

    public void SetPlane(Collider plane)
    {
        this.plane = plane;
    }

    public bool IsSearching()
    {
        return searchingForPath;
    }

    public float GetCurrentSearchTime()
    {
        return currentSearchTime;
    }

    private void OnEnable()
    {
        agents.Add(this);
    }

    private void OnDisable()
    {
        agents.Remove(this);
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        timeUntilNextPath = GetRandomPeriod();
    }

    private void Update()
    {
        if (searchingForPath)
        {
            currentSearchTime += Time.deltaTime;
            rend.material = inactiveMaterial;
        }

        if (agent.hasPath)
        {
            searchingForPath = false;
            currentSearchTime = 0;
            rend.material = activeMaterial;
        }

        timeUntilNextPath -= Time.deltaTime;
        if (timeUntilNextPath <= 0)
        {
            timeUntilNextPath = GetRandomPeriod();
            searchingForPath = true;

            ChooseNewDestination();
        }
    }

    private void ChooseNewDestination()
    {
        agent.SetDestination(GetRandomDestination());
    }

    private Vector3 GetRandomDestination()
    {
        Vector3 destination = transform.position - new Vector3(range, 0, range);
        destination.x += UnityEngine.Random.value * range * 2;
        destination.z += UnityEngine.Random.value * range * 2;

        Bounds bounds = plane.bounds;
        if (!bounds.Contains(destination))
        {
            destination = bounds.ClosestPoint(destination);
        }

        return destination;
    }

    private float GetRandomPeriod()
    {
        return minPathFindPeriod + UnityEngine.Random.value * (maxPathFindPeriod - minPathFindPeriod);
    }
}
