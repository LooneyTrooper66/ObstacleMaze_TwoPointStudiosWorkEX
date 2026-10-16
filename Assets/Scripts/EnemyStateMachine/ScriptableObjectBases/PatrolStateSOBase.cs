using UnityEngine;

[CreateAssetMenu(fileName = "PatrolStateSOBase", menuName = "Scriptable Objects/Bases/PatrolStateSOBase")]
public class PatrolStateSOBase : ScriptableObject
{
    protected EnemyBase enemyBase;
    protected GameObject gameObject;


    public virtual void Initialize( GameObject gameObject, EnemyBase enemyBase)
    {
        this.gameObject = gameObject;
        this.enemyBase = enemyBase;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoUpdateLogic() { }
    public virtual void DoExitLogic() { }
}
