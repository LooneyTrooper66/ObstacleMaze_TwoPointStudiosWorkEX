using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RangedChaseState", menuName = "Scriptable Objects/Chase/RangedChaseState")]
public class RangedChaseState : ChaseStateSOBase
{
    // chase player but try to stay certain distance away so can shoot from a distence
    NavMeshAgent agent;
    RangedAggroCheck aggroCheck;
    [Tooltip("this value should be smaller than actual projectile range to account for player movement after shot")]
    [SerializeField] float withinProjectileRange;
    [Tooltip("how close to the player the enemy will get before stopping")]
    [SerializeField] float stopDistance;
    [Tooltip("should be larger than aggro distance")]
    [SerializeField] float deaggroDistance;
    [SerializeField] float attackCooldown;
    float currentAttackTimer; // counts up to reach attackCooldown before can fire again


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        agent = gameObject.GetComponent<NavMeshAgent>();
        aggroCheck = gameObject.GetComponent<RangedAggroCheck>(); 
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        currentAttackTimer = 0;
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        agent.destination = aggroCheck.player.position;
        currentAttackTimer += Time.deltaTime;
        // if player is outside of aggro range change to patrol
        if (Vector3.Distance(aggroCheck.player.position, gameObject.transform.position) >= deaggroDistance) 
            enemyBase.stateManager.ChangeState(enemyBase.patrolState);
        // if player is within shooting range and attack cooldown is complete, swap to attack state
        if (currentAttackTimer >= attackCooldown && Vector3.Distance(aggroCheck.player.position, gameObject.transform.position) <= withinProjectileRange)
            enemyBase.stateManager.ChangeState(enemyBase.attackState);
        // if enemy is getting too close to player, stop
        if (Vector3.Distance(aggroCheck.player.position, gameObject.transform.position) <= stopDistance)
        {
            agent.stoppingDistance = stopDistance;
            Vector3 lookAtPos = new Vector3(aggroCheck.player.position.x, 1f, aggroCheck.player.position.z);
            gameObject.transform.LookAt(lookAtPos);
        }
        else agent.stoppingDistance = 1;
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
        agent.destination = gameObject.transform.position;
    }
}
