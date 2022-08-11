using System.Collections.Generic;
using UnityEngine;

public class SheepOvergrowthHandler : MonoBehaviour
{
    private const float Cooldown = 15f;

    [SerializeField] private TimerView _timerView;

    private List<Sheep> _sheeps;
    private Timer _timer;
    private bool _isTimerStart;

    private void OnEnable()
    {
        _timerView.Completed += OvergrowSheep;
    }

    private void OnDisable()
    {
        _timerView.Completed -= OvergrowSheep;
    }

    private void Awake()
    {
        _sheeps = new List<Sheep>();
        _timer = new Timer();
        _timerView.Init(_timer);
        _isTimerStart = false;
    }

    private void Update()
    {
        if (_timer.TotalTime > 0 && HasShornSheeps())
            _timer.Tick(Time.deltaTime);
    }

    public void AddNewSheep(Sheep sheep)
    {
        _sheeps.Add(sheep);

        if (_isTimerStart == false)
        {
            _isTimerStart = true;
            _timer.StartCountdown(Cooldown);
        }
    }

    private void OvergrowSheep()
    {
        int overgrowSheepCount;
        List<Sheep> tempSheepsList = new List<Sheep>();

        foreach (var sheep in _sheeps)
        {
            if (sheep.IsShorn)
                tempSheepsList.Add(sheep);
        }

        overgrowSheepCount = Random.Range(1, tempSheepsList.Count);

        for (int i = 0; i < overgrowSheepCount; i++)
        {
            tempSheepsList[i].Overgrow();
        }
    }

    private bool HasShornSheeps()
    {
        if (_sheeps.Count == 0)
            return false;

        foreach (var sheep in _sheeps)
        {
            if (sheep.IsShorn)
                return true;
        }

        return false;
    }
}
