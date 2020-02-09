using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A set of items to be populated and referenced at runtime. The set always starts empty.
/// </summary>
/// <typeparam name="T">The type of the items contained in the set</typeparam>
public class RuntimeSet<T> : ScriptableObject
{
    public List<T> Items = new List<T>();

    /// <summary>
    /// Adds the specified item to this set if it is not already present.
    /// </summary>
    /// <param name="item">item to be added to this set</param>
    /// <returns>true if this set did not already contain the specified item</returns>
    public bool Add(T item)
    {
        if (Items.Contains(item)) return false;

        Items.Add(item);
        return true;
    }

    /// <summary>
    /// Removes the specified element from this set if it is present.
    /// </summary>
    /// <param name="item">item to be removed from this set</param>
    /// <returns>true if this set contained the specified item</returns>
    public bool Remove(T item)
    {
        return Items.Remove(item);
    }

    /// <summary>
    /// Removes all items from the set and returns a list of all the items that were removed.
    /// </summary>
    /// <returns>a list of all the items that were removed</returns>
    public List<T> Clear()
    {
        List<T> cleared = new List<T>(Items);
        Items.Clear();
        return cleared;
    }

    private void OnEnable()
    {
        if (Items.Count > 0)
            Debug.LogWarning("Found leftover values in RuntimeSet (cleared). " +
                "Make sure that all items that are added to a RuntimeSet are also removed at the appropriate time");
        Items.Clear();
    }

    public List<T>.Enumerator GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    public static implicit operator List<T>(RuntimeSet<T> runtimeSet)
    {
        return runtimeSet.Items;
    }
}
