using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "BlockerChaseState", menuName = "Scriptable Objects/Chase/BlockerChaseState")]
public class BlockerChaseState : ChaseStateSOBase
{
    // when detects player within forward vision (dot product) will persue
    NavMeshAgent agent;
    BlockerAggroManager aggroManager;
    Vector3 targetPosition;
    [SerializeField] float stopDistance;
    [SerializeField] float deaggroDistance;



    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        agent = gameObject.GetComponent<NavMeshAgent>();
        aggroManager = gameObject.GetComponent<BlockerAggroManager>();
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        targetPosition = aggroManager.player.position + (aggroManager.player.forward * 3);
        agent.destination = targetPosition;

        // if agent gets close enough, stop infront of player
        if (Vector3.Distance(gameObject.transform.position, aggroManager.player.position) <= stopDistance) enemyBase.stateManager.ChangeState(enemyBase.attackState);
        // if player gets far enough away, go back to patrol
        if (Vector3.Distance(gameObject.transform.position, aggroManager.player.position) >= deaggroDistance) enemyBase.stateManager.ChangeState(enemyBase.patrolState);
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
