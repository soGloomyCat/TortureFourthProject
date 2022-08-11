using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QueueHandler))]
[RequireComponent(typeof(ShopAnimationHandler))]
public class ShopTable : MonoBehaviour
{
    [SerializeField] private List<Place> _shopPlaces;
    [SerializeField] private Transform _rewardPosition;

    private QueueHandler _queueHandler;
    private ShopAnimationHandler _animationHandler;

    public Transform RewardPosition => _rewardPosition;

    private void Awake()
    {
        _queueHandler = GetComponent<QueueHandler>();
        _animationHandler = GetComponent<ShopAnimationHandler>();
    }

    public bool TryAcceptWool()
    {
        foreach (var place in _shopPlaces)
        {
            if (place.IsOccupaded == false)
                return true;
        }

        return false;
    }

    public void Accept(Wool wool, Transform startPosition)
    {
        foreach (var place in _shopPlaces)
        {
            if (place.IsOccupaded == false)
            {
                place.Accept(wool, startPosition);
                _animationHandler.ActivateAnimation();
                break;
            }
        }

    }

    public void Accept(Buyer buyer)
    {
        _queueHandler.Accept(buyer);
    }

    public Wool TakeWool()
    {
        foreach (var place in _shopPlaces)
        {
            if (place.IsReadyToIssue == true)
            {
                return place.GetWool();
            }
        }

        return null;
    }
}
