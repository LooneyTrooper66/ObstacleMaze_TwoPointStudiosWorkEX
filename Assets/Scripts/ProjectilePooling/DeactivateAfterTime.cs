using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
	float deathTimer = 5;
    float timeAlive = 0;


    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= deathTimer)
        {
            gameObject.SetActive(false);
        }
    }
}
