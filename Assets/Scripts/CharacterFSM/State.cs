using UnityEngine;
using Zenject;

public abstract class State : ScriptableObject
{
    [SerializeField] protected string _animationName = "";
    [SerializeField] protected int _animationLayer = 0;
    [SerializeField] protected float _crossFadeTime = 1f;

    protected State _idleState;
    protected State _moveState;

    [Inject]
    private void Construct(IdleState idleState, MoveState moveState)
    {
        _idleState = idleState;
        _moveState = moveState;
    }

    protected abstract void SwitchCheck(CharacterFSM controller);
    public abstract void EnterState(CharacterFSM controller); 
    public abstract void UpdateState(CharacterFSM controller);
    protected abstract void ExitState(CharacterFSM controller); 
    protected void SwitchToState(CharacterFSM controller, State newState)
    { 
        controller.CurrentState?.ExitState(controller);
        controller.SetState(newState);
        controller.CurrentState.EnterState(controller);
    }
}
