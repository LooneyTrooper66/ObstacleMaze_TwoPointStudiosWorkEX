using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CustomizeableProcGen;

public class ProcGenUIManager : MonoBehaviour
{
    public CustomizeableProcGen gen;
    [Header("UI Stuff")]
    //[SerializeField] Dropdown chooseMapType;
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

    public void ChangeMapType(int index)
    {
        switch (index)
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
        gen.waitSeconds = waitSecondsSlider.value;
        waitSecondsText.text = "Grid Offset: " + gen.waitSeconds.ToString();
    }
    public void RegenMap()
    {
        gen.RegenerateMapButton();
    }

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
        //chooseMapType.value = 0;
        mapSizeXSlider.maxValue = 15;
        mapSizeXSlider.value = 2;
        mapSizeXText.text = "Map Size X: 2";
        mapSizeYSlider.maxValue = 15;
        mapSizeYSlider.value = 2;
        mapSizeYText.text = "Map Size Y: 2";
        gridOffsetSlider.maxValue = 40;
        gridOffsetSlider.value = 20;
        gridOffsetText.text = "Grid Offset: 20";
        gridOffsetSlider.value = 0.2f;
        waitSecondsText.text = "Seconds Between Spawn: 0.2";
        //rotateOnOff.isOn = false;
    }
    void SetHeightDefaults()
    {
        //chooseMapType.value = 1;
        mapSizeXSlider.maxValue = 25;
        mapSizeXSlider.value = 10;
        mapSizeXText.text = "Map Size X: 10";
        mapSizeYSlider.maxValue = 25;
        mapSizeYSlider.value = 10;
        mapSizeYText.text = "Map Size Y: 10";
        gridOffsetSlider.maxValue = 5;
        gridOffsetSlider.value = 1;
        gridOffsetText.text = "Grid Offset: 1";
        gridOffsetSlider.value = 0.2f;
        waitSecondsText.text = "Seconds Between Spawn: 0.2";
        //rotateOnOff.isOn = false;
    }
}
