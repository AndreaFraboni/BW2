using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [SerializeField] private float _currentTime = 0;

    public bool isGameStarted = false;

    public Action<int> OnTimeUpdate;

    public bool isGameRunning = false;

    public int time_elapsed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetGameStarted(bool state)
    {
        isGameStarted = state;
        isGameRunning = true;
        OnTimeUpdate?.Invoke(0);
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
        //Debug.Log("STOP TIME");

        isGameRunning = false;
        isGameStarted = false;      

        IOManager.Instance.SetPlayerTime(time_elapsed);
                
       // OnTimeUpdate?.Invoke(0);
        _currentTime = 0;
    }
}
