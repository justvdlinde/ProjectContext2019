using ServiceLocatorNamespace;
using UnityEngine;

public class ScenarioTestItem : MonoBehaviour
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
        if(flag.Hash == requiredFlag)
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
        flagsService.AddFlag(completedFlag);
        gameObject.SetActive(false);
    }
}
