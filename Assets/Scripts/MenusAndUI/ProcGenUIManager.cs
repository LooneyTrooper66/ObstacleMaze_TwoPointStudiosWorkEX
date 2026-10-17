using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CustomizeableProcGen;

public class ProcGenUIManager : MonoBehaviour
{
    public CustomizeableProcGen gen;
    [Header("UI Stuff")]
    [SerializeField] TMP_Dropdown chooseMapType;
    [SerializeField] Button regenerateMap;
    [SerializeField] Toggle rotateOnOff;
    [SerializeField] Slider mapSizeXSlider;
    [SerializeField] TMP_Text mapSizeXText;
    [SerializeField] Slider mapSizeYSlider;
    [SerializeField] TMP_Text mapSizeYText;
    [SerializeField] Slider gridOffsetSlider;
    [SerializeField] TMP_Text gridOffsetText;
    [SerializeField] Slider waitSecondsSlider;
    [SerializeField] TMP_Text waitSecondsText;


    private void Start()
    {
        SetMazeDefaults();
    }

    #region UI functions
    public void ChangeMapType()
    {
        switch (chooseMapType.value)
        {
            case 0: PickedMaze(); break;
            case 1: PickedHeight(); break;
        }
    }
    public void RotateOnOffButton()
    {
        gen.rotateMap = rotateOnOff;
    }
    public void AdjustSizeXSlider()
    {
        gen.mapSizeX = (int)mapSizeXSlider.value;
        mapSizeXText.text = "Map Size X: " + gen.mapSizeX.ToString();
    }
    public void AdjustSizeYSlider()
    {
        gen.mapSizeY = (int)mapSizeYSlider.value;
        mapSizeYText.text = "Map Size Y: " + gen.mapSizeY.ToString();
    }
    public void AdjustGridOffsetSlider()
    {
        gen.gridOffset = (int)gridOffsetSlider.value;
        gridOffsetText.text = "Grid Offset: " + gen.gridOffset.ToString();
    }
    public void AdjustWaitTimeSlider()
    {
        gen.waitSeconds = waitSecondsSlider.value / 10;
        waitSecondsText.text = "Spawn Delay " + gen.waitSeconds.ToString();
    }
    public void RegenMap()
    {
        gen.RegenerateMapButton();
    }
    #endregion

    void PickedMaze()
    {
        gen.SetMazeDefaults();
        SetMazeDefaults();
        gen.RegenerateMapButton();
    }
    void PickedHeight()
    {
        gen.SetHeightDefaults();
        SetHeightDefaults();
        gen.RegenerateMapButton();
    }

    void SetMazeDefaults()
    {
        mapSizeXSlider.value = 5;
        mapSizeXText.text = "Map Size X: 5";
        mapSizeYSlider.value = 5;
        mapSizeYText.text = "Map Size Y: 5";
        gridOffsetSlider.value = 4;
        gridOffsetText.text = "Grid Offset: 4";
        waitSecondsSlider.value = 2;
        waitSecondsText.text = "Spawn Delay: 0.2";
        //rotateOnOff.isOn = false;
    }
    void SetHeightDefaults()
    {
        mapSizeXSlider.value = 5;
        mapSizeXText.text = "Map Size X: 5";
        mapSizeYSlider.value = 5;
        mapSizeYText.text = "Map Size Y: 5";
        gridOffsetSlider.value = 2;
        gridOffsetText.text = "Grid Offset: 2";
        waitSecondsSlider.value = 2;
        waitSecondsText.text = "Spawn Delay: 0.2";
        //rotateOnOff.isOn = false;
    }
}
