using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SheepAnimationHandler))]
public class SheepMover : MonoBehaviour
{
    private SheepAnimationHandler _animator;
    private NavMeshAgent _agent;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _animator = GetComponent<SheepAnimationHandler>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _agent.destination) <= 0.1f)
            _animator.ActivateIdleAnimation();
        else
            _animator.DeactivateIdleAnimation();
    }

    public void Inizialize(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _agent.SetDestination(_targetPosition);
    }
}
