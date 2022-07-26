using UnityEngine;

public class BuyerAnimationHandler : MonoBehaviour
{
    private const string IdleAnimationTrigger = "IsIdle";
    private const string WalkAnimationTrigger = "IsWalk";

    [SerializeField] private ParticleSystem _smileParticle;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ActivateIdleAnimation()
    {
        _animator.SetBool(IdleAnimationTrigger, true);
        _animator.SetBool(WalkAnimationTrigger, false);
    }

    public void ActivateWalkAnimation()
    {
        _animator.SetBool(IdleAnimationTrigger, false);
        _animator.SetBool(WalkAnimationTrigger, true);
    }

    public void ActivateSmiles(Transform spawnPoint)
    {
        _smileParticle = Instantiate(_smileParticle);
        _smileParticle.transform.position = spawnPoint.position;
        _smileParticle.Play();
    }
}
