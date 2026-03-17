using System;
using UnityEngine;

public class TimeManager : GenericSingleton<TimeManager>
{
    [SerializeField] private float _currentTime = 0;

    public bool isGameStarted = false;

    public Action<int> OnTimeUpdate;   

    public void SetGameStarted(bool state)
    {
        isGameStarted = state;
    }

    private void Update()
    {
        if (isGameStarted)
        {
            _currentTime += Time.deltaTime;
            int secondiTrascorsi = (int)_currentTime;
            OnTimeUpdate?.Invoke(secondiTrascorsi);
        }
    }

}
