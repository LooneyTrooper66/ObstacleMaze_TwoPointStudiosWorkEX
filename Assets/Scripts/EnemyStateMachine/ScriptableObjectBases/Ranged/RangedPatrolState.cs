using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RangedPatrolState", menuName = "Scriptable Objects/Patrol/RangedPatrolState")]
[RequireComponent(typeof(RangedAggroCheck))]
public class RangedPatrolState : PatrolStateSOBase
{
    // 3 point patrol route, only swap to attack if player within forward vision (use dot product)
    NavMeshAgent agent;
    RangedAggroCheck aggroCheck;
    int currentPoint;
    MeshRenderer renderer;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        agent = gameObject.GetComponent<NavMeshAgent>();
        aggroCheck = gameObject.GetComponent<RangedAggroCheck>();
        renderer = gameObject.GetComponent<MeshRenderer>();
        currentPoint = 0;
        //patrolPoints = GameObject.Find(aggroCheck.thisEnemyPointHolder).GetComponentsInChildren<Transform>(); // 100% need to update this method later
        /*for (int i = 0; i < patrolPoints.Length; i++ )
            Debug.Log(patrolPoints[i].name);*/
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        aggroCheck.enabled = true;
        renderer.material = aggroCheck.patrolMat;

        // to prevent errors/conflicts between last states agent destination
        agent.destination = aggroCheck.patrolPoints[currentPoint].position;
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        if (Vector3.Distance(agent.transform.position, aggroCheck.patrolPoints[currentPoint].position) <= 2)
        {
            currentPoint++;
            if (currentPoint >= aggroCheck.patrolPoints.Length) currentPoint = 0;
            agent.destination = aggroCheck.patrolPoints[currentPoint].position;
        }

        if (aggroCheck.playerInAggroDistance && aggroCheck.playerInVisionRadius) enemyBase.stateManager.ChangeState(enemyBase.chaseState);
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
        aggroCheck.enabled = false;
        renderer.material = aggroCheck.aggroMat;
    }
}
