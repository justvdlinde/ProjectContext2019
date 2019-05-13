using ServiceLocatorNamespace;
using UnityEngine;
using UnityEngine.Events;

public class RoomEntryScenarioCheck : MonoBehaviour
{
    [SerializeField, ScenarioFlag] private int[] trueConditions;
    [SerializeField, ScenarioFlag] private int[] falseConditions;
    [SerializeField] private UnityEvent onConditionsTrue;
    [SerializeField] private UnityEvent onConditionsFalse;

    private ScenarioFlagsService flagService;
    
    private void Awake()
    {
        flagService = ServiceLocator.Instance.Get<ScenarioFlagsService>() as ScenarioFlagsService;

        if (ConditionsAreMet())
        {
            onConditionsTrue.Invoke();
        }
        else
        {
            onConditionsFalse.Invoke();
        }
    }

    private bool ConditionsAreMet()
    {
        bool condition = false;
        foreach (int flag in trueConditions)
        {
            condition = flagService.FlagConditionHasBeenMet(flag);
            if (condition == false)
            {
                return false;
            }
        }

        foreach(int flag in falseConditions)
        {
            condition = flagService.FlagConditionHasBeenMet(flag);
            if(condition == true)
            {
                return false;
            }
        }

        return true;
    }
}
