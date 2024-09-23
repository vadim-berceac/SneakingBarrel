using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class FSMAIController : MonoBehaviour, IFSMInput
{
    [Header("Agent Settings")]
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _randomNavMeshPointRadius = 15f;
    [SerializeField] private float _minPointSpawnInterval = 3f;
    [SerializeField] private float _maxPointSpawnInterval = 12f;
    [SerializeField] private float _acceleration = 2f;
    [SerializeField] private float _deceleration = 60f;
    [SerializeField] private float _closeEnoughMeters = 4f;

    [Header("Alarm Settings")]
    [SerializeField] private GameObject _alarmSign;

    private float _currentPointSpawnInterval;
    private float _timer = 0f;
    private readonly float _errorOffset = 0.1f;
    private Vector3 _rotationDirection = Vector3.zero;
    private bool _canAttack = false;
    private Transform _cashedTransform;
    private float _basicAgentSpeed;

    public NavMeshAgent Agent => _agent;
    public bool IsMoving => _agent.velocity.sqrMagnitude > 0;
    public float CurrentVelocity => _agent.velocity.sqrMagnitude;
    public Vector3 RotationDirection => _rotationDirection;
    public bool CanAttack => _canAttack;
    public bool IsAttacked => false;

    private void Awake()
    {
        _basicAgentSpeed = _agent.speed;  
        _cashedTransform = transform;
        _currentPointSpawnInterval = Random.Range(_minPointSpawnInterval, _maxPointSpawnInterval);
    }

    private void Update()
    {
        UpdateRotationDirection();
        UpdateVelocity();
        UpdateAttack(); 
        _timer += Time.deltaTime;
        if (_timer >= _currentPointSpawnInterval)
        {
            _timer = 0f;
            _currentPointSpawnInterval = Random.Range(_minPointSpawnInterval, _maxPointSpawnInterval);
            UpdateNewNavmeshWalkablePoint();
        }
    }

    private void UpdateNewNavmeshWalkablePoint()
    {
        Vector3 randomPoint = GetRandomPointOnNavMesh();
        _agent.SetDestination(randomPoint);
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _randomNavMeshPointRadius;
        randomDirection += _cashedTransform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _randomNavMeshPointRadius, NavMesh.AllAreas);
        return hit.position;
    }

    private void UpdateRotationDirection()
    {        
        Vector3 targetDirection;
        if (_fieldOfView.PlayerPosition != Vector3.zero)
        {
            targetDirection = _fieldOfView.PlayerPosition - _cashedTransform.position;
        }
        else
        {
            targetDirection = _agent.steeringTarget - _cashedTransform.position;
        }

        _rotationDirection = targetDirection.sqrMagnitude < _errorOffset * _errorOffset ? Vector3.zero : targetDirection.normalized;
    }

    private void UpdateVelocity()
    {
        if (!_agent.hasPath) { return; }
        _agent.acceleration = (_agent.remainingDistance < _closeEnoughMeters) ? _deceleration : _acceleration;
    }

    private void UpdateAttack()
    {
        if(_fieldOfView.PlayerPosition == Vector3.zero)
        {
            _alarmSign.SetActive(false);
            _canAttack = false;
            _agent.speed = _basicAgentSpeed;
            return;
        }
        _alarmSign.SetActive(true);
        _agent.speed = 0;
        _canAttack = true;
    }
}
