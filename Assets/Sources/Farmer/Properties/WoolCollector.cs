using System;
using System.Collections.Generic;
using UnityEngine;

public class WoolCollector : MonoBehaviour
{
    private const int _maxCount = 5;

    [SerializeField] private Transform _pool;

    private List<Wool> _wools;
    private Wool _lastWool;

    public event Action<int> WoolsCountChanged;
    public event Action WoolPicked;
    public event Action OverloadReached;
    public event Action OverloadRemoved;

    public int MaxCount => _maxCount;
    public int CurrentCount => _wools.Count;
    public Transform Position => _lastWool.transform;

    private void Awake()
    {
        _wools = new List<Wool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TrashBox trashBox))
        {
            OverloadRemoved?.Invoke();
            ClearPool();
            WoolsCountChanged?.Invoke(_wools.Count);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ShopTable shopTable) && HasFreePlace(shopTable))
        {
            OverloadRemoved?.Invoke();
            shopTable.Accept(_lastWool, _lastWool.transform);
            _wools.Remove(_lastWool);
            WoolsCountChanged?.Invoke(_wools.Count);

            if (_wools.Count >= 1)
                _lastWool = _wools[_wools.Count - 1];
            else
                _lastWool = null;
        }
    }

    public void Add(Transform startPlace, Wool wool)
    {
        Wool tempWool = Instantiate(wool, _pool);
        WoolPicked?.Invoke();

        if (_wools.Count == 0)
            tempWool.Cut(startPlace, _pool);
        else
            tempWool.Cut(startPlace, _lastWool.RackPoint);

        _wools.Add(tempWool);
        _lastWool = tempWool;
        WoolsCountChanged?.Invoke(_wools.Count);

        if (_wools.Count == _maxCount)
            OverloadReached?.Invoke();
    }

    private bool HasFreePlace(ShopTable shopTable)
    {
        if (_lastWool != null && shopTable.TryAcceptWool())
            return true;

        return false;
    }

    private void ClearPool()
    {
        if (_wools.Count == 0)
            return;

        foreach (var wool in _wools)
            Destroy(wool.gameObject);

        _wools.Clear();
        _lastWool = null;
    }
}
