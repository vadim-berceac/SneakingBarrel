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
        SwitchCheck(controller);
        Rotate(controller);
        if(controller.Input.CurrentVelocity > _animationMinSpeed)
        {
            controller.Animator.SetFloat("CurrentVelocity", controller.Input.CurrentVelocity);
        }
        else
        {
            controller.Animator.SetFloat("CurrentVelocity", _animationMinSpeed);
        }
    }

    private void Rotate(CharacterFSM controller)
    {
        Quaternion lookRotation = Quaternion.LookRotation
            (new Vector3(controller.Input.RotationDirection.x, 0, controller.Input.RotationDirection.z));
        controller.CachedTransform.rotation = Quaternion.Slerp
            (controller.CachedTransform.rotation, lookRotation, Time.deltaTime * 5);
    }
}
