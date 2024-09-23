using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class FSMPlayerController : MonoBehaviour, IFSMInput
{
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CharacterFSM _characterFSM;
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator _barrelAnimator;
    private InputAction _moveAction;
    private Transform _cashedTransform;
    private Vector3 _targetDirection;
    private Vector3 _moveVector;
    private Vector3 _lastDirection;
    private float _lerpTime = 0;
    private Vector3 _rotationDirection = Vector3.zero;
    private readonly float _smoothing = 0.25f;
    private bool _isAttacked = false;

    public NavMeshAgent Agent => _agent;
    public bool IsMoving => _moveVector != Vector3.zero;
    public CharacterFSM CharacterFSM => _characterFSM;
    public Vector3 RotationDirection => _rotationDirection;
    public float CurrentVelocity => _agent.velocity.sqrMagnitude;
    public bool CanAttack => false;
    public bool IsAttacked => _isAttacked;

    private void Awake()
    {
        _cashedTransform = transform;
        _moveAction = _inputActions.FindAction("Move");        
        _moveAction.started += HandleMoveAction;
        _moveAction.performed += HandleMoveAction;
        _moveAction.canceled += HandleMoveAction;
        _moveAction.Enable();
        AttackState.OnPlayerAttack += OnPlayerIsAttacked;
    }

    private void HandleMoveAction(InputAction.CallbackContext context)
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        _moveVector = new(input.x, 0, input.y);
    }

    private void Update()
    {
        if (_isAttacked) { return; }
        _collider.enabled = IsMoving;
        UpdateMovement();
        UpdateRotationDirection();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _moveAction.started -= HandleMoveAction;
        _moveAction.performed -= HandleMoveAction;
        _moveAction.canceled -= HandleMoveAction;
        AttackState.OnPlayerAttack -= OnPlayerIsAttacked;
    }

    private void UpdateMovement()
    {
        _moveVector.Normalize();
        if(_moveVector != _lastDirection)
        {
            _lerpTime = 0;
        }
        _lastDirection = _moveVector;
        _targetDirection = Vector3.Lerp(_targetDirection, 
            _moveVector, Mathf.Clamp01(_lerpTime * (1 - _smoothing)));

        _agent.Move(_targetDirection * _agent.speed * Time.deltaTime);

        _lerpTime += Time.deltaTime;
    }

    private void UpdateRotationDirection()
    {
        if(_moveVector != Vector3.zero)
        {
            _rotationDirection = _moveVector;
        }        
    }

    private void OnPlayerIsAttacked()
    {
        _isAttacked = true;
        _barrelAnimator.Play("Explosion");
    }
}
