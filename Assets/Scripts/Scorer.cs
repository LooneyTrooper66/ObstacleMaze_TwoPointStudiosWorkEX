using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
	int hits = 0;
	[SerializeField] ParticleSystem cogParticles;
    Mover mover;



    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    private void OnCollisionEnter(Collision collision)
	{
		IHittable hitObj = collision.gameObject.GetComponent<IHittable>();
        if (hitObj != null)
        {
            // adds to the hit counter and prints out the message with the new number at the end
            hits++;
			//Debug.Log($"You've bumped into this many things: {hits}");
			hitObj.ChangeColour();
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 contactPos = contactPoint.point;
            PlayParticles(contactPos);
            mover.RestartPlayer();
        }
	}

    void PlayParticles(Vector3 colPoint)
    {
        cogParticles.Clear();
        cogParticles.transform.position = colPoint;
        cogParticles.Play();
    }
}
