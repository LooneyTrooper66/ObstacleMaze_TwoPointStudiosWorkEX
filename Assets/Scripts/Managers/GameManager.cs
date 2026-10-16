using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager i { get; private set; }

	public float roundDuration = 4;


    private void Awake()
    {
        if (i != null && i != this) Destroy(this);
        else i = this;
        DontDestroyOnLoad(gameObject);
    }
}
