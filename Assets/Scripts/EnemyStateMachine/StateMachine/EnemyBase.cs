using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public StateMachineManager stateManager { get; set; }
	public PatrolState patrolState { get; set; }
	public ChaseState chaseState { get; set; }
	public AttackState attackState { get; set; }

    // scriptable object states
    [SerializeField] PatrolStateSOBase patrolBase;
    [SerializeField] ChaseStateSOBase chaseBase;
    [SerializeField] AttackStateSOBase attackBase;
    public PatrolStateSOBase patrolBaseInstance { get; set; }
    public ChaseStateSOBase chaseBaseInstance { get; set; }
    public AttackStateSOBase attackBaseInstance { get; set; }

    private void Awake()
    {
        patrolBaseInstance = Instantiate(patrolBase);
        chaseBaseInstance = Instantiate(chaseBase);
        attackBaseInstance = Instantiate(attackBase);

        stateManager = new StateMachineManager();
        patrolState = new PatrolState(this, stateManager);
        chaseState = new ChaseState(this, stateManager);
        attackState = new AttackState(this, stateManager);
    }
    private void Start()
    {
        patrolBaseInstance.Initialize(gameObject, this);
        chaseBaseInstance.Initialize(gameObject, this);
        attackBaseInstance.Initialize(gameObject, this);

        stateManager.Initialize(patrolState);
    }

    private void Update()
    {
        stateManager.currentState.FrameUpdate();
    }
}
