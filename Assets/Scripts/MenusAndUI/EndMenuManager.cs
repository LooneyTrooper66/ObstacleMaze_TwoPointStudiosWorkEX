using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    public void RestartButtonPress()
    {
        SceneManager.LoadScene("ProcGenLevel");
    }

    public void MenuButtonPress()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
