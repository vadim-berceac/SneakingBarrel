using UnityEngine;
using Zenject;

public abstract class State : ScriptableObject
{
    [SerializeField] protected string _animationName = "";
    [SerializeField] protected int _animationLayer = 0;
    [SerializeField] protected float _crossFadeTime = 1f;

    protected State _idleState;
    protected State _moveState;
    protected State _aimState;
    protected State _attackState;

    [Inject]
    private void Construct(IdleState idleState, MoveState moveState,
        AttackState attackState, AimState aimState)
    {
        _idleState = idleState;
        _moveState = moveState;
        _aimState = aimState;
        _attackState = attackState;
    }

    protected abstract void SwitchCheck(CharacterFSM controller);
    public abstract void EnterState(CharacterFSM controller); 
    public virtual void UpdateState(CharacterFSM controller)
    {
        Rotate(controller);
        SwitchCheck(controller);
    }
    protected abstract void ExitState(CharacterFSM controller); 
    protected void SwitchToState(CharacterFSM controller, State newState)
    { 
        controller.CurrentState?.ExitState(controller);
        controller.SetState(newState);
        controller.CurrentState.EnterState(controller);
    }

    private void Rotate(CharacterFSM controller)
    {
        Quaternion lookRotation = Quaternion.LookRotation
            (new Vector3(controller.Input.RotationDirection.x, 0, controller.Input.RotationDirection.z));
        controller.CachedTransform.rotation = Quaternion.Slerp
            (controller.CachedTransform.rotation, lookRotation, Time.deltaTime * 3);
    }
}
