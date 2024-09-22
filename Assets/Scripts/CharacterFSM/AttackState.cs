using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Scriptable Objects/AttackState")]
public class AttackState : State
{
    public override void EnterState(CharacterFSM controller)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(CharacterFSM controller)
    {
        base.UpdateState(controller);
    }

    protected override void ExitState(CharacterFSM controller)
    {
        throw new System.NotImplementedException();
    }

    protected override void SwitchCheck(CharacterFSM controller)
    {
        throw new System.NotImplementedException();
    }
}
