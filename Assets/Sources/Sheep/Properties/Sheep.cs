using UnityEngine;

[RequireComponent(typeof(SheepMover))]
[RequireComponent(typeof(SheepMaterialChanger))]
[RequireComponent(typeof(SheepAnimationHandler))]
public class Sheep : MonoBehaviour
{
    [SerializeField] private Wool _woolPrefab;

    private SheepMover _mover;
    private SheepMaterialChanger _materialChanger;
    private SheepAnimationHandler _animationHandler;
    private bool _isShorn;

    public bool IsShorn => _isShorn;

    private void Awake()
    {
        _mover = GetComponent<SheepMover>();
        _materialChanger = GetComponent<SheepMaterialChanger>();
        _animationHandler = GetComponent<SheepAnimationHandler>();
        _isShorn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Mower mower))
        {
            if (_isShorn == false && mower.TryCut())
            {
                _isShorn = true;
                _materialChanger.ChangeMaterial();
                _animationHandler.ActivateShornAnimation();
                mower.Cut(transform, _woolPrefab);
            }
        }
    }

    public void Inizialize(Vector3 targetPosition)
    {
        _mover.Inizialize(targetPosition);
    }

    public void Overgrow()
    {
        _isShorn = false;
        _materialChanger.ResumeMaterial();
    }
}
