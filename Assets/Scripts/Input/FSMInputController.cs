using UnityEngine;
using UnityEngine.AI;

public class FSMInputController : MonoBehaviour, IFSMInput
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _pointSpawnInterval = 5f;
    public float _acceleration = 2f;
    public float _deceleration = 60f;
    public float _closeEnoughMeters = 4f;
    private float _timer = 0f;
    private readonly float _error = 0.1f;
    private Vector3 _rotationDirection = Vector3.zero;
    private Transform _cashedTransform;
    public bool IsMoving => _agent.velocity.sqrMagnitude > 0;
    public float CurrentVelocity => _agent.velocity.sqrMagnitude;
    public Vector3 RotationDirection => _rotationDirection;
    public bool Attack => throw new System.NotImplementedException();

    private void Awake()
    {
        _cashedTransform = transform;
    }

    private void Update()
    {
        UpdateRotationDirection();
        UpdateVelocity();
        _timer += Time.deltaTime;
        if (_timer >= _pointSpawnInterval)
        {
            _timer = 0f;
            SpawnNewPoint();
        }
    }

    private void SpawnNewPoint()
    {
        Vector3 randomPoint = GetRandomPointOnNavMesh();
        _agent.SetDestination(randomPoint);
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 10f;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 10f, NavMesh.AllAreas);
        return hit.position;
    }

    private void UpdateRotationDirection()
    {
        Vector3 targetDirection = _agent.steeringTarget - _cashedTransform.position;
        if (targetDirection.sqrMagnitude < _error * _error)
        {
            _rotationDirection = Vector3.zero;
        }
        else
        {
            _rotationDirection = targetDirection.normalized;
        }
    }

    private void UpdateVelocity()
    {
        if (!_agent.hasPath) { return; }
        _agent.acceleration = (_agent.remainingDistance < _closeEnoughMeters) ? _deceleration : _acceleration;
    }
}
