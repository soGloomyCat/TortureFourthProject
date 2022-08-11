using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FarmerAnimationHandler : MonoBehaviour
{
    private const string AnimationMoveTrigger = "IsWalk";
    private const string AnimationCargoTrigger = "IsMowe";

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void EnableHangAnimation() => _animator.SetBool(AnimationCargoTrigger, true);

    public void EnableMoveAnimation() => _animator.SetBool(AnimationMoveTrigger, true);

    public void DisableHangAnimation() => _animator.SetBool(AnimationCargoTrigger, false);

    public void DisableMoveAnimation() => _animator.SetBool(AnimationMoveTrigger, false);
}
