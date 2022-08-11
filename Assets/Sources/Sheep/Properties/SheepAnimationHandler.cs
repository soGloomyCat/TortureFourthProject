using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SheepAnimationHandler : MonoBehaviour
{
    private const string IdleAnimationTrigger = "IsIdle";
    private const string ShornAnimationTrigger = "IsShorn";

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void ActivateIdleAnimation() => _animator.SetBool(IdleAnimationTrigger, true);

    public void DeactivateIdleAnimation() => _animator.SetBool(IdleAnimationTrigger, false);

    public void ActivateShornAnimation() => _animator.SetTrigger(ShornAnimationTrigger);
}
