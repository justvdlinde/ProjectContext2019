using UnityEngine;

public class ScenarioFlag : ScriptableObject
{
    public const int None = 0;

    public int Hash { get; private set; }

    public string description;
    public bool isChecked;

    public void SetHash(int hash)
    {
        Hash = hash;
    }
}