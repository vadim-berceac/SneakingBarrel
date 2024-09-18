using UnityEngine;

[CreateAssetMenu(fileName = "RotationState", menuName = "Scriptable Objects/RotationState")]
public class RotationState : State
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
        if (controller.Input.GetDirection() == Vector2.zero)
        {
           SwitchToState(controller, _idleState);
        }
    }

    public override void UpdateState(CharacterFSM controller)
    {
        Debug.Log("test rotation");
        SwitchCheck(controller);
        RotateCharacter(controller, controller.Input.GetDirection());
    }

    private void RotateCharacter(CharacterFSM controller, Vector2 direction)
    { 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        controller.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 
    }
}
