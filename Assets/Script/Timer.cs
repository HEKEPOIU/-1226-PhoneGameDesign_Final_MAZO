using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _totalTime = 1;
    [SerializeField] private bool _autoStart = false;
    [SerializeField] private bool _loop = false;
    public float TotalTime { get => _totalTime; set => _totalTime = value; }
    private float _currentTime = 0;
    public event Action OnTimerEnd;

    private void Start()
    {
        if (_autoStart)
        {
            StartTimer();
        }
        else
        {
            this.enabled = false;
        }
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _totalTime)
        {
            _currentTime = 0;
            if (_loop) return;
            this.enabled = false;
            OnTimerEnd?.Invoke();
        }
    }

    public void StartTimer()
    {
        this.enabled = true;
        _currentTime = 0;
    }
}