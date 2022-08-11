using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField] private MissionCanvas _missionCanvas;
    [SerializeField] private Mission _nextMission;
    [SerializeField] private Sprite _targetIcon;
    [SerializeField] private string _missionTargetText;
    [SerializeField] private int _targetCount;
    [SerializeField] private string _type;

    private MissionCanvas _tempCanvas;
    private int _currentTargetCount;
    private bool _isCompleted;

    public bool IsCompleted => _isCompleted;
    public string Type => _type;

    public void Inizialize()
    {
        _isCompleted = false;
        _currentTargetCount = 0;
        _tempCanvas = Instantiate(_missionCanvas);
        _tempCanvas.ActivateMission(_missionTargetText, _currentTargetCount, _targetCount, _targetIcon);
    }

    public void UpdateInfo()
    {
        _tempCanvas.UpdateInfo(++_currentTargetCount, _targetCount);

        if (_currentTargetCount >= _targetCount)
            _isCompleted = true;
    }

    public Mission GetNextMission()
    {
        return _nextMission;
    }

    public void Complete()
    {
        _tempCanvas.Complete();
    }
}
