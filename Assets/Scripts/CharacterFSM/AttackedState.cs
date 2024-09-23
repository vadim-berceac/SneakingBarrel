using UnityEngine;

[CreateAssetMenu(fileName = "AttackedState", menuName = "Scriptable Objects/AttackedState")]
public class AttackedState : State
{
    public override void EnterState(CharacterFSM controller)
    {
        controller.Animator.StopPlayback();
        controller.Animator.CrossFade(_animationName, _crossFadeTime, _animationLayer);
    }

    protected override void ExitState(CharacterFSM controller)
    {
       
    }

    protected override void SwitchCheck(CharacterFSM controller)
    {
        
    }
}
