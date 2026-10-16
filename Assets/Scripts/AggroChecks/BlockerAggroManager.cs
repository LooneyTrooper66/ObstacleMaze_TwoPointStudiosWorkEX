using UnityEngine;
using System.Collections.Generic;

public class BlockerAggroManager : MonoBehaviour
{
    public Transform player;
    public Renderer[] floorContainer;
    public bool playerInVisionRadius;
    public bool playerInAggroDistance;
    public Material patrolMat;
    public Material aggroMat;
    [Tooltip("must be between -1 and 1, sweet spot around 0.4")]
    [SerializeField] float visionRadius;
    [SerializeField] float aggroDistance;

    private void Awake()
    {
        playerInVisionRadius = false;
        playerInAggroDistance = false;
        this.enabled = false;
    }
    private void OnDisable()
    {
        playerInVisionRadius = false;
        playerInAggroDistance = false;
    }

    private void Update()
    {
        // use dot product to check if player is within aggro vision
        Vector3 toPlayer = Vector3.Normalize(player.position - gameObject.transform.position);
        if (Vector3.Dot(gameObject.transform.forward, toPlayer) > visionRadius) playerInVisionRadius = true;
        else playerInVisionRadius = false;

        // use vector3.dist to check if player is within aggro distance
        if (Vector3.Distance(player.position, gameObject.transform.position) <= aggroDistance) playerInAggroDistance = true;
        else playerInAggroDistance = false;
    }
}
