using System;
using System.Collections;
using UnityEngine;

public class FarmerWallet : MonoBehaviour
{
    private const string MissionType = "Service";

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Money _moneyPrefab;

    private int _balance;
    private Coroutine _coroutine;
    private SheepSpawnerHandler _tempSpawnHandler;

    public event Action PaymentReached;
    public event Action<int> BalanceChanged;
    public event Action<int> NeedShowReward;
    public event Action<string> BuyerServiced;

    public Transform SpawnPoint => _spawnPoint;
    public int Balance => _balance;

    private void Awake()
    {
        _balance = 0;
        BalanceChanged?.Invoke(_balance);
    }

    private IEnumerator TakeMoney(Transform finalPosition, int cost)
    {
        WaitForSeconds waiter = new WaitForSeconds(0.03f);
        int spawnedMoneyCount = 0;

        while (spawnedMoneyCount < cost)
        {
            Money tempReward = Instantiate(_moneyPrefab);
            tempReward.transform.position = _spawnPoint.position;
            tempReward.PrepairMove(finalPosition);
            spawnedMoneyCount++;
            _tempSpawnHandler.ReduceCurrentCost();
            yield return waiter;
        }

        PayNewSheep(cost);
    }

    public void PayNewSheep(int cost)
    {
        PaymentReached?.Invoke();
        _balance -= cost;
        BalanceChanged?.Invoke(_balance);
        _tempSpawnHandler.ResumeCurrentCost();
        _tempSpawnHandler = null;
    }

    public void AcceptReward(int reward)
    {
        _balance += reward;
        BuyerServiced?.Invoke(MissionType);
        BalanceChanged?.Invoke(_balance);
        NeedShowReward?.Invoke(reward);
    }

    public void StopPayment()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void PrepairPayment(SheepSpawnerHandler spawnerHandler)
    {
        _tempSpawnHandler = spawnerHandler;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(TakeMoney(_tempSpawnHandler.MoneyPosition, _tempSpawnHandler.Cost));
    }
}
