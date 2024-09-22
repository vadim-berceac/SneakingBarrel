using UnityEngine;
using Zenject;

public class CharacterFSM : MonoBehaviour 
{
    [SerializeField] private bool _isAI = false;
    [SerializeField] private IFSMInput _input;
    [SerializeField] private Animator _animator;

    private Transform _cachedTransform;
    private State _currentState = null;

    public bool IsAI => _isAI;
    public IFSMInput Input => _input;
    public Animator Animator => _animator;
    public Transform CachedTransform => _cachedTransform;
    public State CurrentState => _currentState;

    [Inject]
    private void Construct(IdleState idleState)
    {
        _currentState = idleState; 
    }

    private void Awake()
    {
        _input = GetComponent<IFSMInput>();
        _cachedTransform = transform;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SetState(State state)
    {
        _currentState = state;
    }
}