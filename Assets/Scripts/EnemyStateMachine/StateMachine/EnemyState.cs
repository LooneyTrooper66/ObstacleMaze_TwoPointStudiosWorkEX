using UnityEngine;

public class EnemyState : MonoBehaviour
{
	protected EnemyBase enemyBase;
	protected StateMachineManager stateManager;


    // passes in data when instance of script is created
    public EnemyState(EnemyBase enemyBase, StateMachineManager stateManager)
	{
		this.enemyBase = enemyBase;
		this.stateManager = stateManager;
	}

	public virtual void EnterState() { }
	public virtual void FrameUpdate() { }
	public virtual void ExitState() { }
}
