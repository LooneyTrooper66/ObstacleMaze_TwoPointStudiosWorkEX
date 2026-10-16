using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "BlockerAttackState", menuName = "Scriptable Objects/Attack/BlockerAttackState")]
public class BlockerAttackState : AttackStateSOBase
{
    // when close enough will start to get infront of player and stop to try and make player hit them
    NavMeshAgent agent;
    BlockerAggroManager aggroManager;
    [SerializeField] float chaseDistance;

    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        agent = gameObject.GetComponent<NavMeshAgent>();
        aggroManager = gameObject.GetComponent<BlockerAggroManager>();
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        agent.destination = agent.destination;

    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        if (Vector3.Distance(gameObject.transform.position, aggroManager.player.position) >= chaseDistance) enemyBase.stateManager.ChangeState(enemyBase.chaseState);

    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
