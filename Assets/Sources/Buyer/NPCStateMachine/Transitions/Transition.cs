using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    private Buyer _buyer;

    public Buyer Buyer => _buyer;
    public State NextState => _nextState;
    public bool NeedTransit { get; protected set; }

    private void Awake()
    {
        NeedTransit = false;
        _buyer = GetComponent<Buyer>();
    }
}
