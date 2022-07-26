using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShopTable))]
public class QueueHandler : MonoBehaviour
{
    [SerializeField] private FarmerWallet _farmerWallet;
    [SerializeField] private Transform _buyPosition;

    private ShopTable _shop;
    private List<Buyer> _queue;
    private Buyer _currentBuyer;

    public Buyer CurrentBuyer => _currentBuyer;
    public Transform BuyPosition => _buyPosition;
    public Transform WalletPosition => _farmerWallet.SpawnPoint;
    public Transform CashRegister => _shop.RewardPosition;

    private void Awake()
    {
        _shop = GetComponent<ShopTable>();
        _queue = new List<Buyer>();
    }

    public void Accept(Buyer buyer)
    {
        buyer.Get(this);
        _queue.Add(buyer);

        if (_currentBuyer == null)
        {
            _currentBuyer = buyer;
            _currentBuyer.NeedTakeReward += _farmerWallet.AcceptReward;
        }
    }

    public Buyer GetBuyer(Buyer currentBuer)
    {
        int currentBuyerIndex = _queue.IndexOf(currentBuer);

        if (currentBuyerIndex > 0)
            return _queue[currentBuyerIndex - 1];

        return null;
    }

    public void ClearQueue(Buyer currentBuyer)
    {
        currentBuyer.NeedTakeReward -= _farmerWallet.AcceptReward;
        _queue.Remove(currentBuyer);
        _currentBuyer = null;

        if (_queue.Count > 0)
        {
            _currentBuyer = _queue[0];
            _currentBuyer.NeedTakeReward += _farmerWallet.AcceptReward;

            foreach (var buyer in _queue)
            {
                buyer.ChangeFrontBuyer();
            }
        }
    }

    public Wool TakeWool()
    {
        return _shop.TakeWool();
    }
}
