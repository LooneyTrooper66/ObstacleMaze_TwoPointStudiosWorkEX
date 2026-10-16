using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BlockerPatrolState", menuName = "Scriptable Objects/Patrol/BlockerPatrolState")]
[RequireComponent(typeof(BlockerAggroManager))]
public class BlockerPatrolState : PatrolStateSOBase
{
    // wonder randomly around map
    NavMeshAgent agent;
    BlockerAggroManager aggroManager;
    List<Bounds> navmeshBounds = new List<Bounds>();
    Vector3 randomPoint;
    [SerializeField] float patrolSpeed;
    [SerializeField] float patrolAccel;
    [SerializeField] float chaseSpeed;
    [SerializeField] float chaseAccel;
    MeshRenderer renderer;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        agent = gameObject.GetComponent<NavMeshAgent>();
        aggroManager = gameObject.GetComponent<BlockerAggroManager>();
        renderer = gameObject.GetComponent<MeshRenderer>();
        // using an array to allow for navmeshes from multiple tiles 
        // (im assuming i'll need to use tiles for the procedural gen)
        foreach (Renderer go in aggroManager.floorContainer)
        {
            navmeshBounds.Add(go.bounds);
        }
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        aggroManager.enabled = true;
        agent.speed = patrolSpeed;
        agent.acceleration = patrolAccel;
        renderer.material = aggroManager.patrolMat;
        ChooseRandomPoint();
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        if (Vector3.Distance(randomPoint, gameObject.transform.position) <= 2) ChooseRandomPoint();
        if (aggroManager.playerInAggroDistance && aggroManager.playerInVisionRadius) enemyBase.stateManager.ChangeState(enemyBase.chaseState);
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
        aggroManager.enabled = false;
        agent.speed = chaseSpeed;
        agent.acceleration = chaseAccel;
        renderer.material = aggroManager.aggroMat;
    }

    public void ChooseRandomPoint()
    {
        Bounds randomSurface = navmeshBounds[Random.Range(0, navmeshBounds.Count)];
        float rx = Random.Range(randomSurface.min.x, randomSurface.max.x);
        float rz = Random.Range(randomSurface.min.z, randomSurface.max.z);
        randomPoint = new Vector3(rx, gameObject.transform.position.y, rz);
        agent.SetDestination(randomPoint);
    }
}
