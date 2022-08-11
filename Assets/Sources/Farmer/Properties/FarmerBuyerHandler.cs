using System;
using UnityEngine;

public class FarmerBuyerHandler : MonoBehaviour
{
    private const string MissionType = "Collect";

    [SerializeField] private FarmerWallet _wallet;

    private bool _isPayStarted;
    private SheepSpawnerHandler _tempSpawnerHandler;

    public event Action<string> SheepBuyed;

    private void OnEnable()
    {
        _wallet.PaymentReached += PayNewSheep;
    }

    private void OnDisable()
    {
        _wallet.PaymentReached -= PayNewSheep;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out SheepSpawnerHandler spawnerHandler) && spawnerHandler.TryBuyNewSheep(_wallet.Balance))
        {
            _wallet.StopPayment();
            _isPayStarted = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out SheepSpawnerHandler spawnerHandler) && spawnerHandler.TryBuyNewSheep(_wallet.Balance))
        {
            if (_isPayStarted == false)
            {
                _isPayStarted = true;
                _tempSpawnerHandler = spawnerHandler;
                _wallet.PrepairPayment(spawnerHandler);
            }
        }
    }

    private void PayNewSheep()
    {
        SheepBuyed?.Invoke(MissionType);
        _tempSpawnerHandler.BuyNewSheep();
        _isPayStarted = false;
    }
}
