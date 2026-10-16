using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [SerializeField] ParticleSystem confettiParticles;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WinConfetti());
        }
    }

    IEnumerator WinConfetti()
    {
        confettiParticles.Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("WinScreen");
    }
}
