using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
	// holds all angles for spinner obj
	[SerializeField] float xAngle = 0;
	[SerializeField] float yAngle = 0;
    [SerializeField] float zAngle = 0;


	void Update()
	{
		// should really be in its own function but I dont think thats part of what I needed to fix
		transform.Rotate(xAngle, yAngle, zAngle);
	}
}
