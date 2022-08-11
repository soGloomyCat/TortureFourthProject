using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShopAnimationHandler : MonoBehaviour
{
    private const string AnimationTrigger = "Accept";

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void ActivateAnimation() => _animator.SetTrigger(AnimationTrigger);
}
