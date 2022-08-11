using System;
using UnityEngine;

public class SheepSpawnerHandler : MonoBehaviour
{
    private const int TriggerValue = 17;
    private const int CostIncrease = 15;

    [SerializeField] private SheepSpawner _spawner;
    [SerializeField] private Transform _moneyPosition;
    [SerializeField] private ParticleSystem _particleSystem;

    private int _cost;
    private int _currentCost;
    private int _currentSheepCount;
    private float _cooldown;
    private float _currentTime;

    public event Action<int> CostChanged;
    public event Action<int> CurrentCostChanged;

    public int Cost => _cost;
    public Transform MoneyPosition => _moneyPosition;

    private void Awake()
    {
        _cost = 0;
        _currentCost = _cost;
        CostChanged?.Invoke(_cost);
        _cooldown = 0.8f;
        _currentTime = _cooldown;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
    }

    public bool TryBuyNewSheep(int wallet)
    {
        if (wallet >= _cost && _currentTime >= _cooldown)
        {
            return true;
        }

        return false;
    }

    public void BuyNewSheep()
    {
        _particleSystem.Play();
        _spawner.Spawn();
        _currentTime = 0;

        if (++_currentSheepCount >= TriggerValue)
        {
            _cost += CostIncrease;
            _currentCost = _cost;
            CostChanged?.Invoke(_cost);
        }
    }

    public void ReduceCurrentCost()
    {
        CurrentCostChanged?.Invoke(--_currentCost);
    }

    public void ResumeCurrentCost()
    {
        _currentCost = _cost;
    }
}
