using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] float timeLeft = 4;
    [SerializeField] TMP_Text timerText;


    private void Start()
    {
        // checking for game manager to stop errors when testing without entering menu first
        if (GameManager.i != null) timeLeft = GameManager.i.roundDuration * 60;
        else timeLeft = timeLeft * 60;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        UpdateTimerUI();

        if (timeLeft <= 0) SceneManager.LoadScene("LoseScreen");
    }

    void UpdateTimerUI()
    {
        float minutes = timeLeft / 60;
        int minutesI = (int)minutes;
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0} : {1:00}", minutesI, seconds);
    }
}
