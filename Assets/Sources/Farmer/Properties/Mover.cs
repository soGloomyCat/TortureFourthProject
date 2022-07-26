using UnityEngine;

[RequireComponent(typeof(FarmerAnimationHandler))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Mower _mower;
    [SerializeField] private ParticleSystem _clouds;
    [SerializeField] private Joystick _joystick;

    private FarmerAnimationHandler _animationHandler;

    private void Awake()
    {
        _animationHandler = GetComponent<FarmerAnimationHandler>();
        _mower.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Move();
            _animationHandler.EnableMoveAnimation();
            _clouds.Play();
        }
        if (Input.anyKey == false)
        {
            _animationHandler.DisableMoveAnimation();
            _clouds.Stop();
        }
    }

    private void Move()
    {
        var horizontal = _joystick.Horizontal;
        var vertical = _joystick.Vertical;
        var direction = new Vector3(horizontal, 0, vertical);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _walkSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MoweZone moweZone))
        {
            _animationHandler.EnableHangAnimation();
            _mower.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MoweZone moweZone))
        {
            _animationHandler.DisableHangAnimation();
            _mower.gameObject.SetActive(false);
        }
    }
}
