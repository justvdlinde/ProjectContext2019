using ServiceLocatorNamespace;
using UnityEngine;

public class ScenarioItem : MonoBehaviour
{
    public ScenarioStatus Status => status;
    [SerializeField] private ScenarioStatus status;

    [SerializeField, ScenarioFlag] private int requiredFlag;
    [SerializeField, ScenarioFlag] private int completedFlag;

    private ScenarioFlagsService flagsService;
    private new Renderer renderer;
    
    private void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        flagsService = (ScenarioFlagsService)ServiceLocator.Instance.Get<ScenarioFlagsService>();

        if(requiredFlag != ScenarioFlag.None && !flagsService.FlagConditionHasBeenMet(requiredFlag))
        {
            renderer.enabled = false;
        }

        flagsService.FlagAdded += OnFlagAdded;
    }

    private void OnDisable()
    {
        flagsService.FlagAdded -= OnFlagAdded;
    }

    private void OnFlagAdded(ScenarioFlag flag)
    {
        Debug.Log(gameObject.name);
        Debug.Log("on flag added " + flag);
        Debug.Log("requredFlag " + requiredFlag);

        if(flag.hash == requiredFlag)
        {
            StartScenario();
        }
    }

    public void StartScenario()
    {
        status = ScenarioStatus.InProgress;
        renderer.enabled = true;
    }

    public void CompleteScenario()
    {
        status = ScenarioStatus.Completed;

        Debug.Log("Completed scenario, flag hash: " + completedFlag);

        flagsService.AddCheckedFlag(completedFlag);

        gameObject.SetActive(false);
    }
}
