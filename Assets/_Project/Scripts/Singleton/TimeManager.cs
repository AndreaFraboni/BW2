using System;
using UnityEngine;

public class TimeManager : GenericSingleton<TimeManager>
{
    [SerializeField] private float _currentTime = 0;

    public bool isGameStarted = false;

    public Action<int> OnTimeUpdate;

    public bool isGameRunning = false;

    public int time_elapsed;

    public void SetGameStarted(bool state)
    {
        isGameStarted = state;
        isGameRunning = true;
    }

    private void Update()
    {
        if (isGameStarted && isGameRunning)
        {
            _currentTime += Time.deltaTime;
            time_elapsed = (int)_currentTime;
            OnTimeUpdate?.Invoke(time_elapsed);
        }
    }
    public void StopTimer()
    {
        isGameRunning = false;

        IOManager.Instance.SetPlayerTime(time_elapsed);  
    }
}
