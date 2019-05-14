using System;
using UnityEngine;

public static class GameTimeManager 
{
    public enum TimeState
    {
        Paused = 0,
        Normal = 1
    }

    public static Action<TimeState> GameTimeStateChanged;
    public static TimeState CurrentTimeState = TimeState.Normal;

    public static void PauseGame()
    {
        CurrentTimeState = TimeState.Paused;
        Time.timeScale = (int)CurrentTimeState;
        GameTimeStateChanged?.Invoke(CurrentTimeState);
    }

    public static void ResumeGame()
    {
        CurrentTimeState = TimeState.Normal;
        Time.timeScale = (int)CurrentTimeState;
        GameTimeStateChanged?.Invoke(CurrentTimeState);
    }
}
