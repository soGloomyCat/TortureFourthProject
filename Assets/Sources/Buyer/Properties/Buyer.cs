using System;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    private QueueHandler _queue;
    private Transform _enterPosition;
    private Transform _exitPosition;

    public event Action NeedChange;
    public event Action<int> NeedTakeReward;

    public QueueHandler Queue => _queue;
    public Transform EnterPosition => _enterPosition;
    public Transform ExitPosition => _exitPosition;

    public void InizializeParameters(Transform enterPosition, Transform exitPosition)
    {
        _enterPosition = enterPosition;
        _exitPosition = exitPosition;
    }

    public void Get(QueueHandler queue)
    {
        _queue = queue;
    }

    public void ChangeFrontBuyer()
    {
        NeedChange?.Invoke();
    }

    public void TakeReward(int reward)
    {
        NeedTakeReward?.Invoke(reward);
    }
}
