public class LeaveState : State
{
    private void Start()
    {
        AnimationHandler.ActivateWalkAnimation();
        Mover.ActivateAgent();
        Mover.SetTarget(Buyer.ExitPosition);
    }

    private void Update()
    {
        if (transform.position == Mover.Destination)
            Destroy(gameObject);
    }
}
