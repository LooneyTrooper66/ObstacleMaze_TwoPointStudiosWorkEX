using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(EnemyBase enemyBase, StateMachineManager stateManager) : base(enemyBase, stateManager)
    {
        // acts as void awake
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.patrolBaseInstance.DoEnterLogic();

    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.patrolBaseInstance.DoUpdateLogic();
    }
    public override void ExitState()
    {
        base.ExitState();
        enemyBase.patrolBaseInstance.DoExitLogic();
    }
}
