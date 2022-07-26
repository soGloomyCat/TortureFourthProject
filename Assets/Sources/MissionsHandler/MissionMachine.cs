using System.Collections;
using UnityEngine;

public class MissionMachine : MonoBehaviour
{
    private const string MissionType = "Collect";
    private const float TriggerDistance = 2.5f;

    [SerializeField] private Farmer _farmer;
    [SerializeField] private Mission _startMission;
    [SerializeField] private SheepSpawnerHandler _spawnerHandler;
    [SerializeField] private Arrow _leadArrow;

    private Mission _currentMission;
    private Mission _nextMission;
    private bool _isMissionsChange;
    private bool _isArrowActive;
    private Coroutine _coroutine;

    public Mission CurrentMission => _currentMission;

    private void OnEnable()
    {
        _farmer.MissionInfoUpdated += UpdateMissionInfo;
    }

    private void OnDisable()
    {
        _farmer.MissionInfoUpdated -= UpdateMissionInfo;
    }

    private void Start()
    {
        _currentMission = _startMission;

        if (_currentMission.Type.ToLower() == MissionType.ToLower())
            ActivateSpawner();

        _currentMission.Inizialize();
        _isMissionsChange = false;
    }

    private void Update()
    {
        if (_currentMission.IsCompleted && _isMissionsChange == false)
            PrepaireChangeMission();

        if (_isArrowActive)
            MoveArrow();
    }

    private void UpdateMissionInfo(string missionType)
    {
        if (_currentMission.Type.ToLower() == missionType.ToLower())
            _currentMission.UpdateInfo();
    }

    private void PrepaireChangeMission()
    {
        _isMissionsChange = true;
        _nextMission = _currentMission.GetNextMission();
        _currentMission.Complete();

        if (_currentMission.Type.ToLower() == MissionType.ToLower())
            DeactivateSpawner();

        if (_nextMission != null)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeMission());
        }
    }

    private IEnumerator ChangeMission()
    {
        WaitForSeconds waiter = new WaitForSeconds(1f);
        _currentMission = _nextMission;

        if (_currentMission.Type.ToLower() == MissionType.ToLower())
            ActivateSpawner();

        yield return waiter;
        _currentMission.Inizialize();
        _nextMission = null;
        _isMissionsChange = false;
    }

    private void ActivateSpawner()
    {
        _spawnerHandler.gameObject.SetActive(true);
        _leadArrow.gameObject.SetActive(true);
        _isArrowActive = true;
    }

    private void DeactivateSpawner()
    {
        _spawnerHandler.gameObject.SetActive(false);
        _isArrowActive = false;
    }

    private void MoveArrow()
    {
        _leadArrow.transform.position = new Vector3(_farmer.transform.position.x, _leadArrow.transform.position.y, _farmer.transform.position.z);

        if (Vector3.Distance(_leadArrow.transform.position, _spawnerHandler.transform.position) < TriggerDistance)
            _leadArrow.gameObject.SetActive(false);
        else
            _leadArrow.gameObject.SetActive(true);
    }
}
