using System;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    [SerializeField] private FarmerWallet _wallet;
    [SerializeField] private FarmerBuyerHandler _buyer;

    public event Action<string> MissionInfoUpdated;

    private void OnEnable()
    {
        _wallet.BuyerServiced += UpdateMissionInfo;
        _buyer.SheepBuyed += UpdateMissionInfo;
    }

    private void OnDisable()
    {
        _wallet.BuyerServiced += UpdateMissionInfo;
        _buyer.SheepBuyed += UpdateMissionInfo;
    }

    private void UpdateMissionInfo(string missionType)
    {
        MissionInfoUpdated?.Invoke(missionType);
    }
}
