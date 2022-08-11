using UnityEngine;

public class Place : MonoBehaviour
{
    private bool _isOccupaded;
    private bool _isReadyToIssue;
    private Wool _currentWool;

    public bool IsOccupaded => _isOccupaded;
    public bool IsReadyToIssue => _isReadyToIssue;

    private void Awake()
    {
        _isOccupaded = false;
        _isReadyToIssue = false;
    }

    public void Accept(Wool wool, Transform startPosition)
    {
        _isOccupaded = true;
        wool.transform.parent = transform;
        _currentWool = wool;
        _currentWool.ReadyToIssue += ChangePlaceStatus;
        _currentWool.Cut(startPosition, transform);
    }

    public Wool GetWool()
    {
        Wool tempWool = _currentWool;

        if (tempWool.IsReadyToMove)
        {
            _isOccupaded = false;
            _isReadyToIssue = false;
            _currentWool = null;
            return tempWool;
        }

        return null;
    }

    private void ChangePlaceStatus()
    {
        _currentWool.ReadyToIssue -= ChangePlaceStatus;
        _isReadyToIssue = true;
    }
}
