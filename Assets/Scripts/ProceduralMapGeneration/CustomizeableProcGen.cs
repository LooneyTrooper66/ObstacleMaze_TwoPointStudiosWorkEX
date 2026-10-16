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
	public enum MapType
	{
		maze,
		height
	}
	public MapType type;

    public string piecesToUse;
	public float waitSeconds;

	public int mapSizeX;
	public int mapSizeY;
	public int gridOffset;
    public bool rotateMap;

    List<GameObject> currentMapBits = new List<GameObject>();


    private void Start()
    {
        SetMazeDefaults();
		MoveCamera(); // need to move camera so will frame whole map before map generation starts
        StartCoroutine(GenerateMap());
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
                yield return new WaitForSeconds(waitSeconds);
            }
        }
    }
	public void SetMazeDefaults()
	{
		type = MapType.maze;
        piecesToUse = "mazePool";
		mapSizeX = 2;
		mapSizeY = 2;
		gridOffset = 20;
		waitSeconds = 0.2f;
		rotateMap = false;
    }
    public void SetHeightDefaults()
    {
        type = MapType.height;
        piecesToUse = "cubePool";
        mapSizeX = 10;
        mapSizeY = 10;
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

    }

    public void RegenerateMapButton()
    {
        MoveCamera(); 
        ClearCurrentMap();
        StartCoroutine(GenerateMap());
    }
}
