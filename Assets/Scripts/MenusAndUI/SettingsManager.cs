using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
	[SerializeField] Slider roundDurationSlider;
	[SerializeField] TMP_Text roundDurationText;


    private void Start()
    {
		// could use player prefs to save and load settings later
		roundDurationSlider.value = 4;
        roundDurationText.text = "4 Minutes";
    }

    public void OnSliderChange()
	{
		float newTime = roundDurationSlider.value;
		GameManager.i.roundDuration = newTime;
		roundDurationText.text = newTime.ToString() + " Minutes";
	}
}
