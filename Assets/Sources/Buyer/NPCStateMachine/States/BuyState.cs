using UnityEngine;

public class BuyState : State
{
    private const int MinWoolCount = 3;
    private const int MaxWoolCount = 8;
    private const float TriggerDistance = 0.05f;

    private int _needWoolCount;
    private bool _isServiced;

    public bool IsSurviced => _isServiced;

    private void OnEnable()
    {
        Buyer.NeedChange += SetFrontBuyer;
    }

    private void Start()
    {
        _needWoolCount = Random.Range(MinWoolCount, MaxWoolCount);
        _isServiced = false;
        SetFrontBuyer();
        Mover.ActivateAgent();
    }

    private void Update()
    {
        if (Buyer.Queue.CurrentBuyer == Buyer && Vector3.Distance(transform.position, Mover.Destination) <= TriggerDistance)
        {
            Collector.ActivateCanvas(_needWoolCount);

            if (Collector.RemainWoolCount > 0)
                Collector.Accept(Buyer.Queue.TakeWool());
        }

        if (Collector.IsCollected && RewardGiver.IsStarted == false)
        {
            CompleteService();
        }

        if (Vector3.Distance(transform.position, Mover.Destination) <= TriggerDistance)
        {
            Mover.DeactivateAgent();
            AnimationHandler.ActivateIdleAnimation();
        }
    }

    public void SetFrontBuyer()
    {
        if (Mover.TryChange(Buyer.Queue.GetBuyer(Buyer)))
            Mover.Change(Buyer.Queue.GetBuyer(Buyer));
        else
            Mover.SetTarget(Buyer.Queue.BuyPosition);
    }

    private void CompleteService()
    {
        RewardGiver.PrepairRewardIssue(_needWoolCount, Buyer.Queue.CashRegister, Buyer.Queue.WalletPosition);
        AnimationHandler.ActivateSmiles(RewardGiver.SpawnPoint);
        Buyer.TakeReward(Collector.Cost * _needWoolCount);
        Collector.DeactivateCanvas();
        _isServiced = true;
        Buyer.Queue.ClearQueue(Buyer);
    }
}
