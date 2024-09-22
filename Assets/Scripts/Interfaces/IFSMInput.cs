
using UnityEngine;

public interface IFSMInput
{
    public bool IsMoving { get; }
    public Vector3 RotationDirection { get; }
    public float CurrentVelocity { get; }
    public bool CanAttack {  get; }
}
