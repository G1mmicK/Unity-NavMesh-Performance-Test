using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCounter : MonoBehaviour
{
    [SerializeField]
    private GameObjectRuntimeSet trees = default;

    private void OnEnable()
    {
        trees.Add(this.gameObject);
    }

    private void OnDisable()
    {
        trees.Remove(this.gameObject);
    }
}
