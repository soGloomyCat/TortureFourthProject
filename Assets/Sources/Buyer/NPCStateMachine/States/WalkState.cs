public class WalkState : State
{
    private void Start() => Mover.SetTarget(Buyer.EnterPosition);
}
