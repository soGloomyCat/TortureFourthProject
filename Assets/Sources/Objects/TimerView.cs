using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Image _fillIcon;

    private Timer _timer;

    public event Action Completed;

    public void Init(Timer timer)
    {
        if (_timer != null)
            UnsubscribeTimer();

        _timer = timer;
        SubscribeTimer();
    }

    private void SubscribeTimer()
    {
        _timer.Started += OnTimerStart;
        _timer.Updated += OnTimerUpdate;
        _timer.Completed += OnTimerCompleted;
    }

    private void UnsubscribeTimer()
    {
        _timer.Started -= OnTimerStart;
        _timer.Updated -= OnTimerUpdate;
        _timer.Completed -= OnTimerCompleted;
    }

    private void OnTimerStart()
    {
        _fillIcon.fillAmount = 0;
    }

    private void OnTimerUpdate()
    {
        _fillIcon.fillAmount = _timer.TimeLeft / _timer.TotalTime;
    }

    private void OnTimerCompleted()
    {
        _fillIcon.fillAmount = 0;
        Completed?.Invoke();
    }
}
