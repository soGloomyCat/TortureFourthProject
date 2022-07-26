using System;
using System.Collections;
using UnityEngine;

public class Wool : MonoBehaviour
{
    private const float Speed = 3.5f;

    [SerializeField] private Transform _rackPoint;
    [SerializeField] private int _cost;

    private Coroutine _coroutine;
    private bool _isReadyToMove;

    public event Action ReadyToIssue;

    public Transform RackPoint => _rackPoint;
    public int Cost => _cost;
    public bool IsReadyToMove => _isReadyToMove;

    private IEnumerator Collect(Transform startPosition, Transform rackPoint)
    {
        transform.position = startPosition.position;

        while (transform.position != rackPoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, rackPoint.position, Speed * Time.deltaTime);
            yield return null;
        }

        _isReadyToMove = true;
        ReadyToIssue?.Invoke();
        StopCoroutine(_coroutine);
    }

    public void Cut(Transform startPosition, Transform rackPoint)
    {
        _isReadyToMove = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Collect(startPosition, rackPoint));
    }
}
