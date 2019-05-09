using UnityEngine;

public class ScenarioFlag : ScriptableObject
{
    public const int None = 0;

    public int Hash => hash;

    public string description;
    public bool isChecked;

    [SerializeField] private int hash;

    public void SetHash(int hash)
    {
        this.hash = hash;
    }

    public override int GetHashCode()
    {
        return hash;
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