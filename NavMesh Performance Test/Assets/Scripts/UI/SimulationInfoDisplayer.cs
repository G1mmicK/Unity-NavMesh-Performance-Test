using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SimulationInfoDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObjectRuntimeSet trees = default;
    [SerializeField]
    private AgentRuntimeSet agents = default;
    [SerializeField]
    private string treeCountDisplayText = default;
    [SerializeField]
    private string agentCountDisplayText = default;
    [SerializeField]
    private string searchingDisplayText = default;
    [SerializeField]
    private string searchtimeDisplayText = default;

    private Text text;

    private float recentMaxSearchTime = 0;
    private float timeSinceRecentMax = 0;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        int treeCount = trees.Items.Count;
        int agentCount = agents.Items.Count;
        int searching = 0;

        foreach (RandomPathFindingMovement agent in agents)
        {
            if (agent.IsSearching())
            {
                searching++;
            }

            if (agent.GetCurrentSearchTime() >= recentMaxSearchTime)
            {
                timeSinceRecentMax = 0;
                recentMaxSearchTime = agent.GetCurrentSearchTime();
            }
        }

        timeSinceRecentMax += Time.deltaTime;

        if (timeSinceRecentMax >= 5)
        {
            timeSinceRecentMax = 0;
            recentMaxSearchTime = 0;
        }

        text.text =
            treeCountDisplayText + treeCount + "\n" +
            agentCountDisplayText + agentCount + "\n" +
            searchingDisplayText + searching + "\n" +
            searchtimeDisplayText + recentMaxSearchTime;
    }
}
