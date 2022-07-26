using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Quaternion _tempRotate;
    private Vector3 _tempDirection;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        _tempDirection = _target.position - transform.position;
        _tempRotate = Quaternion.LookRotation(_tempDirection, Vector3.up);
        transform.rotation = new Quaternion(transform.localRotation.x, _tempRotate.y, transform.localRotation.z, _tempRotate.w);
    }
}
