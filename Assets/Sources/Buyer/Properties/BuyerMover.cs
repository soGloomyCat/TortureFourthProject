using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BuyerMover : MonoBehaviour
{
    private const float Offset = 0.5f;

    [SerializeField] private BuyerAnimationHandler _animationHandler;

    private NavMeshAgent _agent;
    private Buyer _target;
    private Transform _position;

    public Vector3 Destination => _agent.destination;
    public Buyer Target => _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_target != null)
            ChangePosition();
    }

    public bool TryChange(Buyer buyer)
    {
        if (buyer != null)
            return true;

        return false;
    }

    public void Change(Buyer buyer)
    {
        _target = buyer;
    }

    public void SetTarget(Transform position)
    {
        _target = null;
        _position = position;
        ActivateAgent();
        _agent.SetDestination(_position.position);
    }

    public void ActivateAgent()
    {
        _agent.isStopped = false;
    }

    public void DeactivateAgent()
    {
        _agent.isStopped = true;
    }

    private void ChangePosition()
    {
        _animationHandler.ActivateWalkAnimation();
        ActivateAgent();
        _agent.SetDestination(new Vector3(_target.transform.position.x, _target.transform.position.y, _target.transform.position.z - Offset));
    }
}
