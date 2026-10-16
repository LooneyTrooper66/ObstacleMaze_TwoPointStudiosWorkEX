using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// why does this exist??? its not used on anything??????????
public class Invisible : MonoBehaviour
{
	[SerializeField] float timeToWait = 5f;

	MeshRenderer rendererRef;

	void Start()
	{
		rendererRef = GetComponent<MeshRenderer>();
		rendererRef.enabled = false;
	}

	void Update()
	{
		if (Time.time > timeToWait)
		{
			rendererRef.enabled = true;
		}
	}
}
