using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineTimeStateHandler : MonoBehaviour
{
    [SerializeField, HideInInspector] private PlayableDirector playableDirector;

    private void OnValidate()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

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
            ResumeTimeline();
        }
        else
        {
            PauseTimeline();
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

