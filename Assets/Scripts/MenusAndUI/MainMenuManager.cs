using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject settingsMenu;
	[SerializeField] GameObject creditsMenu;


    private void Start()
    {
        BackButtonPress();
    }

    public void StartButtonPress()
    {
        SceneManager.LoadScene("ProcGenLevel");
    }
    public void SettingsButtonPress()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void CreditsButtonPress()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    public void QuitButtonPress()
    {
        Application.Quit();
    }
    public void BackButtonPress()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
    public void DemoButtonPress()
    {
        SceneManager.LoadScene("ProcGenDemo");
    }
}
