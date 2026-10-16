
public class StateMachineManager
{
	public EnemyState currentState { get; set; }


	public void Initialize(EnemyState startState)
	{
		currentState = startState;
		currentState.EnterState();
	}

	public void ChangeState(EnemyState newState)
	{
		currentState.ExitState();
		currentState = newState;
		currentState.EnterState();
	}
}
