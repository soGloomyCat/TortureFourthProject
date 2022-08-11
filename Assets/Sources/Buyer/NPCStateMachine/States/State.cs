using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Buyer))]
[RequireComponent(typeof(BuyerAnimationHandler))]
[RequireComponent(typeof(BuyerMover))]
[RequireComponent(typeof(BuyerCollector))]
[RequireComponent(typeof(RewardGiver))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    private Buyer _buyer;
    private BuyerAnimationHandler _animationHandler;
    private BuyerMover _mover;
    private BuyerCollector _collector;
    private RewardGiver _rewardGiver;

    protected BuyerMover Mover => _mover;
    protected Buyer Buyer => _buyer;
    protected BuyerAnimationHandler AnimationHandler => _animationHandler;
    protected BuyerCollector Collector => _collector;
    protected RewardGiver RewardGiver => _rewardGiver;

    private void Awake()
    {
        _mover = GetComponent<BuyerMover>();
        _buyer = GetComponent<Buyer>();
        _animationHandler = GetComponent<BuyerAnimationHandler>();
        _collector = GetComponent<BuyerCollector>();
        _rewardGiver = GetComponent<RewardGiver>();
    }

    public void InizializeState()
    {
        enabled = true;

        foreach (Transition transition in _transitions)
            transition.enabled = true;
    }

    public void FinalizeState()
    {
        foreach (Transition transition in _transitions)
            transition.enabled = false;

        enabled = false;
    }

    public bool ReadyToTransit()
    {
        foreach (Transition transition in _transitions)
        {
            if (transition.NeedTransit)
                return true;
        }

        return false;
    }

    public State GetNextState()
    {
        foreach (Transition transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.NextState;
        }

        return null;
    }
}
