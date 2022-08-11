using System.Collections;
using UnityEngine;

public class Money : MonoBehaviour
{
    private const float Speed = 3f;

    [SerializeField] private Transform _rackPoint;

    private Coroutine _coroutine;

    public Transform RackPoint => _rackPoint;

    public void PrepairMove(Transform target, Transform secondPoint = null)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move(target, secondPoint));
    }

    private IEnumerator Move(Transform target, Transform secondPoint = null)
    {
        WaitForSeconds waiter = new WaitForSeconds(0.05f);

        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);

            if (transform.position == target.position && secondPoint != null)
                target = secondPoint;

            yield return null;
        }

        yield return waiter;
        Destroy(gameObject);
    }
}
