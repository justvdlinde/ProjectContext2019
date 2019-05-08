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

    public override bool Equals(object other)
    {
        if (other is ScenarioFlag)
        {
            return Hash == (other as ScenarioFlag).Hash;
        }
        else if(other is int)
        {
            return Hash == (int)other;
        }
        else
        {
            return false;
        }
    }
}