using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CharacterFSM : MonoBehaviour 
{
    [SerializeField] private bool _isAI = false;
    [SerializeField] private IInputSource _input;
    [SerializeField] private Animator _animator;


    [Header("Character params")]
    [SerializeField] private float _speed = 4f;

    private Transform _cachedTransform;
    private State _currentState = null;

    public IInputSource Input => _input;
    public Animator Animator => _animator;
    public Transform CachedTransform => _cachedTransform;
    public State CurrentState => _currentState;
    public float Speed => _speed;

    [Inject]
    private void Construct(IdleState idleState, AiInput aiInput)
    {
        _currentState = idleState;       
        _input = aiInput;
    }

    private void Awake()
    {
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