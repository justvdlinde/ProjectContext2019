using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    [EnumFlag]
    public ScenarioEnumFlags flags;

    private void Start()
    {
        Scenario.OnScenarioFinished += AddFlag;
    }

    private void AddFlag(ScenarioEnumFlags flag)
    {
        flags |= flag;
    }

    public bool ContainsFlag(ScenarioEnumFlags flag)
    {
        return (flags & flag) != 0;
    }

    public bool ContainsAllLowerFlags(ScenarioEnumFlags flag)
    {
        bool b = (byte)flags >= (byte)flag - (byte)flag / 2;

        Debug.Log("flags " + (byte)flags);
        Debug.Log("flag " + (byte)flag);
        Debug.Log((byte)flag - (byte)flag / 2);
        Debug.Log("is true " + b);
        return b;
    }
}
