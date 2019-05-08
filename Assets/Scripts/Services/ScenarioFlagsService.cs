using ServiceLocatorNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioFlagsService : IService
{
    private const string FLAG_COLLECTION_FILE_PATH = "ScenarioFlags";

    public readonly ScenarioFlagCollection FlagCollection;

    public Action<ScenarioFlag> FlagAdded;

    /// <summary>
    /// Dictionary of all flags, key: hash, value: flag
    /// </summary>
    private Dictionary<int, ScenarioFlag> allFlags = new Dictionary<int, ScenarioFlag>();
    /// <summary>
    /// Hash set for keeping track of the flags that are met
    /// </summary>
    private HashSet<int> checkedFlags = new HashSet<int>();

    public ScenarioFlagsService()
    {
        FlagCollection = Resources.Load<ScenarioFlagCollection>(FLAG_COLLECTION_FILE_PATH);

        allFlags = new Dictionary<int, ScenarioFlag>();
        foreach(ScenarioFlag flag in FlagCollection.collection)
        {
            allFlags.Add(flag.hash, flag);
        }
    }
        
    public bool FlagConditionHasBeenMet(int hash)
    {
        return checkedFlags.Contains(hash);
    }

    public void AddFlag(ScenarioFlag flag)
    {
        AddFlag(flag.hash);
    }

    public void AddFlag(int hash)
    {
        if (hash == ScenarioFlag.None) { return; }

        if (!FlagConditionHasBeenMet(hash))
        {
            checkedFlags.Add(hash);
            ScenarioFlag flag = allFlags[hash];
            flag.isChecked = true;

            Debug.Log("Adding flag " + flag.name);
            FlagAdded?.Invoke(flag);
        }
    }

    public void RemoveFlag(ScenarioFlag flag)
    {
        if (FlagConditionHasBeenMet(flag.hash))
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
