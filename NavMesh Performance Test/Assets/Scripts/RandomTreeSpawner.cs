using System.Collections;
using UnityEngine;

public class RandomTreeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject treePrefab = default;
    [SerializeField]
    private Transform spawnParent = default;
    [SerializeField]
    private Collider plane = default;
    [SerializeField]
    private GameObjectRuntimeSet trees = default;
    [SerializeField]
    private NavMeshBuilder navMeshBuilder = default;
    [Range(0, 10000)]
    [SerializeField]
    private int spawnCount = default;

    public void SpawnTrees()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(treePrefab, GetRandomLocation(treePrefab.transform.position.y), treePrefab.transform.rotation, spawnParent);
        }

        StartCoroutine(BuildNavMeshAfterTime(0.1f));
    }

    public void ClearTrees()
    {
        for (int i = trees.Items.Count - 1; i >= 0; i--)
        {
            Destroy(trees.Items[i]);
        }

        StartCoroutine(BuildNavMeshAfterTime(0.1f));
    }

    private Vector3 GetRandomLocation(float y)
    {
        Vector3 location = plane.bounds.min;
        location.y = y;
        location.x += Random.value * plane.bounds.size.x;
        location.z += Random.value * plane.bounds.size.z;

        return location;
    }

    private IEnumerator BuildNavMeshAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        navMeshBuilder.Build();
    }
}
