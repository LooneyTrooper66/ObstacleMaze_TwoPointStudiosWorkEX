using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(EnemyBase enemyBase, StateMachineManager stateManager) : base(enemyBase, stateManager)
    {
        // acts as void awake
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.attackBaseInstance.DoEnterLogic();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.attackBaseInstance.DoUpdateLogic();
    }
    public override void ExitState()
    {
        base.ExitState();
        enemyBase.attackBaseInstance.DoExitLogic();
    }
}
