using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyBase enemyBase, StateMachineManager stateManager) : base(enemyBase, stateManager)
    {
        // acts as void awake
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.chaseBaseInstance.DoEnterLogic();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.chaseBaseInstance.DoUpdateLogic();
    }
    public override void ExitState()
    {
        base.ExitState();
        enemyBase.chaseBaseInstance.DoExitLogic();
    }
}
