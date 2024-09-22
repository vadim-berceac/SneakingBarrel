using UnityEngine;

[CreateAssetMenu(fileName = "MoveState", menuName = "Scriptable Objects/MoveState")]
public class MoveState : State
{
    private readonly float _animationMinSpeed = 1f;
    public override void EnterState(CharacterFSM controller)
    {
        controller.Animator.CrossFade(_animationName, _crossFadeTime, _animationLayer);
    }

    protected override void ExitState(CharacterFSM controller)
    {
        controller.Animator.SetFloat("CurrentVelocity", _animationMinSpeed);
    }

    protected override void SwitchCheck(CharacterFSM controller)
    {
        if (!controller.Input.IsMoving)
        {
            SwitchToState(controller, _idleState);
        }
    }

    public override void UpdateState(CharacterFSM controller)
    {
        base.UpdateState(controller);
        if(controller.Input.CurrentVelocity > _animationMinSpeed)
        {
            controller.Animator.SetFloat("CurrentVelocity", controller.Input.CurrentVelocity);
        }
        else
        {
            controller.Animator.SetFloat("CurrentVelocity", _animationMinSpeed);
        }
    }
}
