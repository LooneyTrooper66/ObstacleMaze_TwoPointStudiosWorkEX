using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeableProcGen : MonoBehaviour
{
	// This will be used for the demo scene,
	// it will have UI to adjust the size and type of map
	[Header("Main Variables")]
	[SerializeField] Transform camTransform;
    [SerializeField] Pooler pooler;

    public string piecesToUse;

	public int mapSizeX;
	public int mapSizeY;
	public float gridOffset;
    public float waitSeconds;
    public bool rotateMap;

    List<GameObject> currentMapBits = new List<GameObject>();


    private void Start()
    {
        SetMazeDefaults();
		MoveCamera(); // need to move camera so will frame whole map before map generation starts
        //StartCoroutine(GenerateMap());
    }

	void Update()
	{
		if (rotateMap)
		{
			//rotate le map
		}
	}

    IEnumerator GenerateMap()
	{

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeY; z++)
            {
                Vector3 pos = new Vector3(x * gridOffset, 0, z * gridOffset);
                GameObject mapPiece = pooler.SpawnFromPool(piecesToUse, pos, Quaternion.identity);
                mapPiece.transform.SetParent(transform);
                currentMapBits.Add(mapPiece);
                yield return new WaitForSeconds(waitSeconds);
            }
        }
    }
	public void SetMazeDefaults()
	{
        piecesToUse = "mazePool";
		mapSizeX = 5;
		mapSizeY = 5;
		gridOffset = 4;
		waitSeconds = 0.2f;
		rotateMap = false;
    }
    public void SetHeightDefaults()
    {
        piecesToUse = "cubePool";
        mapSizeX = 5;
        mapSizeY = 5;
        gridOffset = 1;
        waitSeconds = 0.2f;
        rotateMap = false;
    }
    void MoveCamera()
	{
		// based on size of grid work out center and move to frame center
		// will need som eextra maths to figure out how high up to go to get whole map in frame
	}

    void ClearCurrentMap()
    {
        for (int i = currentMapBits.Count - 1; i >= 0; i--)
        {
            GameObject toDeactivate = currentMapBits[i];
            currentMapBits.RemoveAt(i);
            toDeactivate.transform.SetParent(null);
            toDeactivate.SetActive(false);
        }
    }

    public void RegenerateMapButton()
    {
        StopCoroutine(GenerateMap());
        ClearCurrentMap();
        MoveCamera();
        StartCoroutine(GenerateMap());
    }
}
