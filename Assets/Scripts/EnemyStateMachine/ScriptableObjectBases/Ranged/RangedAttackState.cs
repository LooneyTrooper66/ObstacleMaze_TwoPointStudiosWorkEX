using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RangedAttackState", menuName = "Scriptable Objects/Attack/RangedAttackState")]
public class RangedAttackState : AttackStateSOBase
{
    // shoot projectiles at player when within certain distance
    NavMeshAgent agent;
    RangedAggroCheck aggroCheck;
    Pooler pooler;
    [SerializeField] string poolTag;
    [SerializeField] float projectileForce;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        agent = gameObject.GetComponent<NavMeshAgent>();
        aggroCheck = gameObject.GetComponent<RangedAggroCheck>();
        pooler = aggroCheck.projectilePool;
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        GameObject projectile = pooler.SpawnFromPool(poolTag, aggroCheck.shootPoint.transform.position, aggroCheck.shootPoint.transform.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(aggroCheck.shootPoint.forward * projectileForce, ForceMode.Impulse); 
        enemyBase.stateManager.ChangeState(enemyBase.chaseState);
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
}
