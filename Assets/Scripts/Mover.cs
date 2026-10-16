using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	// scriptable object containing movement values (so can be edited in playtime)
	[SerializeField] MovementValues moveValues;
	PlayerInput inputActs;
	Rigidbody rb;
	Vector3 moveDirection;
	Transform bodyRotation;
	Vector3 startPos;
	bool playerMoveable;


	// only runs on the first frame of the game
	void Start()
	{
		startPos = new Vector3(transform.position.x, 4, transform.position.z);
		// runs print instructions function on the first frame
		//PrintInstructions();
		rb = GetComponent<Rigidbody>();
		bodyRotation = GetComponent<Transform>();
		playerMoveable = true;

        // clamps move speed to prevent the player from endlessly accelerating
        rb.maxLinearVelocity = moveValues.maxSpeed;
    }

	// handle input system action map enable/disable
    private void OnEnable()
    {
        inputActs = new PlayerInput();
        inputActs.Player.Enable();
    }
    private void OnDisable()
    {
        inputActs.Player.Disable();
    }

    // runs every 50 frames, all physics based logic should be in here
    void FixedUpdate()
	{
		if (playerMoveable) MovePlayer();
	}

	// prints out the instructions to the console in the unity editor
	void PrintInstructions()
	{
		Debug.Log("Welcome to Roger and Out!");
		Debug.Log("Roger is lost and needs your help.");
		Debug.Log("You need to guide Roger out of the technological jungle of Scraptopia! ");
		Debug.Log("Use WASD to move Roger.");
		Debug.Log("Try not to hit the obstacles, they'll give Roger a shock!");
	}

    #region player movement
    // calculates the way the player should move based ono inputs and updates the characters in game position
    void MovePlayer()
	{
		// adds force to the characters rigidbody after calculating direction to move in based on player inputs
		moveDirection = inputActs.Player.Move.ReadValue<Vector3>();
		if (moveDirection == Vector3.zero) DeceleratePlayer();
		else
		{
			rb.AddForce(moveDirection.normalized * moveValues.moveForce, ForceMode.Force);
			TurnPlayer();
		}
    }

	// make character look in direction of his velocity 
	 void TurnPlayer()
	{
		Vector3 lerpRotation = Vector3.Lerp(bodyRotation.forward, moveDirection, moveValues.turnSpeed * Time.deltaTime);
		bodyRotation.rotation = Quaternion.LookRotation(lerpRotation);
	}

	// apply force in opposite direction to make the player feel less slidey
	void DeceleratePlayer()
    {
        if (rb.linearVelocity.magnitude >= 1)
        {
			moveDirection = Vector3.Lerp(rb.linearVelocity, Vector3.zero, moveValues.decelerationSpeed * Time.deltaTime);
			rb.linearVelocity = moveDirection;
        }
	}
	#endregion

	public void RestartPlayer()
	{
		playerMoveable = false;
		rb.linearVelocity = Vector3.zero;
        StopCoroutine(RestartAnimation()); // to make sure they dont stack
        StartCoroutine(RestartAnimation());
	}

	IEnumerator RestartAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        transform.position = startPos;
		yield return new WaitForSeconds(0.5f);
		playerMoveable = true;
    }
}
