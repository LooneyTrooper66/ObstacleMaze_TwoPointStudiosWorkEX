using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
	// time game runs for before ball is dropped
	[SerializeField] float timeToWait = 5f;

	MeshRenderer rendererRef;
	Rigidbody rigid;

	// gets needed components in first frame 
	void Start()
	{
		rendererRef = GetComponent<MeshRenderer>();
		rigid = GetComponent<Rigidbody>();

		rendererRef.enabled = false;
		rigid.useGravity = false;
	}

	// counts up for time game is active, when timer becomes more than timeToWait the
	// renderer is enabled (so you can see the ball) and the balls rigidbody's gravity is turned on so it falls down
	void Update()
	{
		if (Time.time > timeToWait)
		{
			rendererRef.enabled = true;
			rigid.useGravity = true;
		}
	}
}
