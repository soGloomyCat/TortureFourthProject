using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private Transform _pool;
    [SerializeField] private Transform _enterPosition;
    [SerializeField] private Transform _exitPosition;
    [SerializeField] private List<Buyer> _buyers;
    [SerializeField] private float _cooldown;

    private float _currentTime;

    private void Awake()
    {
        _currentTime = _cooldown;
    }

    private void Update()
    {
        if (_currentTime >= _cooldown)
            Spawn();

        _currentTime += Time.deltaTime;
    }

    private void Spawn()
    {
        _currentTime = 0;
        Buyer tempBuyer = Instantiate(_buyers[Random.Range(0, _buyers.Count)], _pool);
        tempBuyer.InizializeParameters(_enterPosition, _exitPosition);
    }
}
