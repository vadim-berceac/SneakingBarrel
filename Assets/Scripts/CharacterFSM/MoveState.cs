using UnityEngine;

[CreateAssetMenu(fileName = "MoveState", menuName = "Scriptable Objects/MoveState")]
public class MoveState : State
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
        Debug.Log("test move");
        SwitchCheck(controller);
        MoveCharacter(controller, controller.Input.GetDirection());
    }

    private void MoveCharacter(CharacterFSM controller, Vector2 direction)
    {
        direction = direction.normalized;
        controller.CachedTransform.Translate(controller.Speed * Time.deltaTime 
            * new Vector3(direction.x, 0, direction.y));
    }
}
