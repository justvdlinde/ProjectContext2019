using ServiceLocatorNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioFlagsService : IService
{
    public readonly ScenarioFlagCollection[] FlagsCollection;

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
        FlagsCollection = Resources.LoadAll<ScenarioFlagCollection>("");
        allFlags = new Dictionary<int, ScenarioFlag>();

        foreach (ScenarioFlagCollection collection in FlagsCollection)
        {
            foreach (ScenarioFlag flag in collection.collection)
            {
                allFlags.Add(flag.Hash, flag);
            }
        }
    }
        
    public bool FlagConditionHasBeenMet(int hash)
    {
        return checkedFlags.Contains(hash);
    }

    public void AddFlag(ScenarioFlag flag)
    {
        AddFlag(flag.Hash);
    }

    public void AddFlag(int hash)
    {
        if (hash == ScenarioFlag.None) { return; }

        if (!FlagConditionHasBeenMet(hash))
        {
            checkedFlags.Add(hash);
            ScenarioFlag flag = allFlags[hash];
            flag.isChecked = true;
            FlagAdded?.Invoke(flag);
            Debug.Log("Flag has been checked: " + flag.name);
        }
    }

    public void RemoveFlag(ScenarioFlag flag)
    {
        if (FlagConditionHasBeenMet(flag.Hash))
        {
            checkedFlags.Remove(flag.Hash);
            flag.isChecked = false;
        }
    }
}
