using UnityEngine;
using UnityEngine.Playables;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    private void OnEnable()
    {
        GameTimeManager.GameTimeStateChanged += OnGameTimeStateChanged;
    }

    private void OnDisable()
    {
        GameTimeManager.GameTimeStateChanged -= OnGameTimeStateChanged;
    }

    private void OnGameTimeStateChanged(GameTimeManager.TimeState timeState)
    {
        if(timeState == GameTimeManager.TimeState.Normal)
        {
            PauseTimeline();
        }
        else
        {
            ResumeTimeline();
        }
    }

    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    public void ResumeTimeline()
    {
        playableDirector.Resume();
    }
}

