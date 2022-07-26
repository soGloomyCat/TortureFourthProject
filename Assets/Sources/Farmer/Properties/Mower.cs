using UnityEngine;

public class Mower : MonoBehaviour
{
    [SerializeField] private WoolCollector _collector;

    public void Cut(Transform startPlace, Wool wool)
    {
        _collector.Add(startPlace, wool);
    }

    public bool TryCut()
    {
        if (_collector.CurrentCount < _collector.MaxCount)
            return true;

        return false;
    }
}
