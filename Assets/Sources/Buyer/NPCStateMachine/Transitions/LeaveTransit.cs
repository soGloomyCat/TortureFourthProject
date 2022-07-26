using UnityEngine;

public class LeaveTransit : Transition
{
    [SerializeField] private BuyState _buyState;

    private void Update()
    {
        if (_buyState.IsSurviced)
            NeedTransit = true;
    }
}
