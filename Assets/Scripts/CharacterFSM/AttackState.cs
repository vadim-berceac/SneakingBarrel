using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AttackState", menuName = "Scriptable Objects/AttackState")]
public class AttackState : State
{
    public static UnityAction OnPlayerAttack;
    public override void EnterState(CharacterFSM controller)
    {
        controller.Animator.StopPlayback();
        controller.Animator.Play(_animationName, _animationLayer);
        OnPlayerAttack?.Invoke();
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

    }
}
