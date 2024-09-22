using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "Scriptable Objects/IdleState")]
public class IdleState : State
{
    public override void EnterState(CharacterFSM controller)
    {
        controller.Animator.CrossFade(_animationName, _crossFadeTime, _animationLayer);
    }

    protected override void ExitState(CharacterFSM controller)
    {
        
    }

    protected override void SwitchCheck(CharacterFSM controller)
    {
        if (controller.Input.CanAttack && controller.IsAI)
        {
            SwitchToState(controller, _aimState);
        }
        if (controller.Input.IsMoving)
        {
            SwitchToState(controller, _moveState);
        }
    }

    public override void UpdateState(CharacterFSM controller)
    {
        base.UpdateState(controller);
    }
}
