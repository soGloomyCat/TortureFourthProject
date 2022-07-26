using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SheepAnimationHandler : MonoBehaviour
{
    private const string AnimationTrigger = "IsIdle";

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void ActivateAnimation() => _animator.SetBool(AnimationTrigger, true);

    public void DeactivateAnimation() => _animator.SetBool(AnimationTrigger, false);
}
