
using UnityEngine;
using UnityEngine.AI;

public interface IFSMInput
{
    public NavMeshAgent Agent { get; }
    public bool IsMoving { get; }
    public Vector3 RotationDirection { get; }
    public float CurrentVelocity { get; }
    public bool CanAttack {  get; }
    public bool IsAttacked { get; }
}
