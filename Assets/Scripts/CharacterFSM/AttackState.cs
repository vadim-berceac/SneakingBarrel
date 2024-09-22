using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Scriptable Objects/AttackState")]
public class AttackState : State
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
        if (controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            SwitchToState(controller, _idleState);
        }
    }
}
