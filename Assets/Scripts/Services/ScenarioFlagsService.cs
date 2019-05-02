using ServiceLocatorNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioFlagsService : IService
{
    private const string FLAG_COLLECTION_FILE_PATH = "ScenarioFlags";

    public Action<ScenarioFlag> FlagAdded;
    public ScenarioFlagCollection flagCollection { get; private set; }

    /// <summary>
    /// Dictionary of all flags, with hash key
    /// </summary>
    private Dictionary<int, ScenarioFlag> allFlags = new Dictionary<int, ScenarioFlag>();

    /// <summary>
    /// Hash set from keeping track of the flags that are met
    /// </summary>
    private HashSet<int> checkedFlags = new HashSet<int>();

    public ScenarioFlagsService()
    {
        flagCollection = Resources.Load<ScenarioFlagCollection>(FLAG_COLLECTION_FILE_PATH);

        allFlags = new Dictionary<int, ScenarioFlag>();
        foreach(ScenarioFlag flag in flagCollection.collection)
        {
            allFlags.Add(flag.hash, flag);
        }
    }

    public bool FlagExistsInCollection(int hash)
    {
        return allFlags.ContainsKey(hash);
    }

    public bool CollectionContainsFlag(int hash, out ScenarioFlag flag)
    {
        bool contains = checkedFlags.Contains(hash);
        if (contains)
        {
            flag = allFlags[hash];
        }

        flag = null;
        return contains;
    }
        
    public bool ContainsCheckedFlag(int hash)
    {
        return checkedFlags.Contains(hash);
    }

    public void AddCheckedFlag(int hash)
    {
        if (hash == ScenarioFlag.None) { return; }

        if (!ContainsCheckedFlag(hash))
        {
            checkedFlags.Add(hash);
            ScenarioFlag flag = allFlags[hash];
            flag.isChecked = true;

            Debug.Log("invokr");
            FlagAdded?.Invoke(flag);
        }
    }

    public void RemoveCheckedFlag(ScenarioFlag flag)
    {
        if (ContainsCheckedFlag(flag.hash))
        {
            checkedFlags.Remove(flag.hash);
            flag.isChecked = false;
        }
        else
        {
            Debug.LogWarningFormat("Does not contain a key of {0} with hash {1}", flag.name, flag.hash);
        }
    }
}
