using UnityEngine;

[CreateAssetMenu(fileName = "AimState", menuName = "Scriptable Objects/AimState")]
public class AimState : State
{
    public override void EnterState(CharacterFSM controller)
    {
        controller.Animator.StopPlayback();
        controller.Animator.Play(_animationName, _animationLayer);
    }

    public override void UpdateState(CharacterFSM controller)
    {
        base.UpdateState(controller);
    }

    protected override void ExitState(CharacterFSM controller)
    {

    }

    protected override void SwitchCheck(CharacterFSM controller)
    {
        if (!controller.Input.CanAttack && controller.IsAI)
        {
            SwitchToState(controller, _idleState);
        }

        if (controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            SwitchToState(controller, _attackState);
        }
    }
}
