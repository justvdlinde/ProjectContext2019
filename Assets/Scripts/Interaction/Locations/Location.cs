using ServiceLocatorNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    [SerializeField] [LocationID] private int id;
    public int ID => id;
    public GameObject GameObject { get { return gObject; } }
    public Collider Collider { get { return collider; } }

    [SerializeField, HideInInspector] private GameObject gObject;
    [SerializeField, HideInInspector] private new Collider collider;
    [SerializeField, ScenarioFlag] private int requiredFlag;
    [SerializeField, ScenarioFlag] private int startedFlag;
    [SerializeField, ScenarioFlag] private int completedFlag;

    private ScenarioStatus status;
    private Sprite[] statusImages;
    private ScenarioFlagsService flagsService;
    private Image statusImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            flagsService.AddFlag(startedFlag);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            flagsService.AddFlag(completedFlag);
        }
    }

    private void OnEnable()
    {
        if (status == ScenarioStatus.NotStarted)
        {
            statusImage.sprite = statusImages[0];
        }

        flagsService = (ScenarioFlagsService)ServiceLocator.Instance.Get<ScenarioFlagsService>();
        flagsService.FlagAdded += OnFlagAdded;
    }

    private void OnDisable()
    {
        flagsService.FlagAdded -= OnFlagAdded;
    }

    private void OnFlagAdded(ScenarioFlag flag)
    {
        if (flag.Hash == requiredFlag)
        {
            StartScenario();
        }
        Debug.Log("Started Scenario");
    }

    public void StartScenario()
    {
        status = ScenarioStatus.InProgress;
        flagsService.AddFlag(startedFlag);
        statusImage.sprite = statusImages[1];
    }

    public void CompleteScenario()
    {
        status = ScenarioStatus.Completed;
        flagsService.AddFlag(completedFlag);
        statusImage.sprite = statusImages[2];
    }

    private void OnValidate()
    {
        if (gObject == null) gObject = gameObject;
        if (collider == null) collider = GetComponent<Collider>();
        if (statusImage == null) statusImage = GetComponent<Image>();

        statusImages = Resources.LoadAll("StatusImages", typeof(Sprite)).Cast<Sprite>().ToArray();
    }
}
