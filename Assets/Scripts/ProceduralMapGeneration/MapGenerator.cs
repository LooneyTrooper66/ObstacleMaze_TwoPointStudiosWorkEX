using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Generation")]
	[SerializeField] int mapSizeX;
	[SerializeField] int mapSizeY;
    [Tooltip("how big the space between each tile should be")]
	[SerializeField] float mapGenOffset;
	[SerializeField] GameObject[] mapPrefabs; // if expanded to remove walls and added in walls after generation could improve map quality/size
    public List<Renderer> mapFloors = new List<Renderer>();
    NavMeshSurface[] surfaces;
    List<EnemyBase> enemiesOnMap = new List<EnemyBase>();

    [Header("Place Entities")]
    [SerializeField] GameObject startPrefab;
    [SerializeField] GameObject finishPrefab;
    [SerializeField] GameObject player;
    [SerializeField] Pooler projectilePooler;


    private void Awake()
    {
        player.SetActive(false);

        // get all enemies somehow?? maybe have enemy spawns on the map instead of them there outright
        // set enemies active false
    }

    private void Start()
    {
        surfaces = GetComponentsInChildren<NavMeshSurface>();

        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeY; z++)
            {
                Vector3 pos = new Vector3(x * mapGenOffset, 0, z * mapGenOffset);
                int randomPiece = Random.Range(0, mapPrefabs.Length);
                GameObject mapPiece = Instantiate(mapPrefabs[randomPiece], pos, CheckRotation(x, z));
                mapPiece.transform.SetParent(transform);
                Renderer floor = mapPiece.GetComponentInChildren<Renderer>(); // should only get first childs gameObject 
                mapFloors.Add(floor);

                EnemyBase[] enemies = mapPiece.GetComponentsInChildren<EnemyBase>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemiesOnMap.Add(enemies[i]);
                    enemies[i].gameObject.SetActive(false);
                }
            }
        }
        GenerateNavmesh(); 
        // have place start/finish tile here,
        // not using for now as is only 4 big and therefore can just place manually
        SpawnEnemies();
        SetPlayerActive();
    }

    void GenerateNavmesh()
    {
        // builds surfaces for each enemy type
        // breaks if regenerate too many times but not important
        // as will only generate once per level load when in game
        foreach (NavMeshSurface surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }

    void SpawnEnemies()
    {
        // need to assign player and projectile holder
        for (int i = 0; i < enemiesOnMap.Count; i++)
        {
            if (enemiesOnMap[i].GetComponent<RangedAggroCheck>())
            {
                RangedAggroCheck thisChecker = enemiesOnMap[i].GetComponent<RangedAggroCheck>();
                thisChecker.player = player.transform;
                thisChecker.projectilePool = projectilePooler;
            }
            else
            {
                BlockerAggroManager thisManager = enemiesOnMap[i].GetComponent<BlockerAggroManager>();
                thisManager.player = player.transform;
                thisManager.floorContainer = mapFloors.ToArray();
            }
            enemiesOnMap[i].gameObject.SetActive(true);
        }
    }

    void SetPlayerActive()
    {
        // would also need to move player to random position 
        player.SetActive(true);
    }

    // will change this when i make the map bigger/change the tiles
    Quaternion CheckRotation(int x, int z)
    {
        //Debug.Log(x  + " " + z);

        if (x == 0 && z == 0)
        {
            //Debug.Log("00 rotation applied");
            return Quaternion.identity;
        }
        else if (x == (mapSizeX - 1) && z == (mapSizeY - 1))
        {
            //Debug.Log("max max rotation applied");
            return Quaternion.Euler(0, 180, 0);
        }
        else if (x == (mapSizeX - 1) && z == 0)
        {
            //Debug.Log("max 0 rotation applied");
            return Quaternion.Euler(0, 270, 0);
        }
        else if (x == 0 && z == (mapSizeY - 1))
        {
            //Debug.Log("0 max rotation applied");
            return Quaternion.Euler(0, 90, 0);
        }
        else
        {
            //Debug.Log("default rotation applied");
            return Quaternion.identity;
        }
    }

    #region Debugging Stuff
    /*private void Update()
    {
        if (regenerateMap)
        {
            regenerateMap = false;
            ClearMap();
            StartCoroutine(WaitForNextTileGeneration());
            //GenerateGrid();
        }
    }

    void ClearMap()
    {
        for (int i = mapFloors.Count - 1; i >= 0; i--)
        {
            // will pool later for more efficiency
            GameObject toDestroy = mapFloors[i];
            mapFloors.RemoveAt(i);
            Destroy(toDestroy);
        }
    }

    // for debugging and showing off
    IEnumerator WaitForNextTileGeneration()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeY; z++)
            {
                Vector3 pos = new Vector3(x * mapGenOffset, 0, z * mapGenOffset);
                int randomPiece = Random.Range(0, mapPrefabs.Length);
                GameObject mapPiece = Instantiate(mapPrefabs[randomPiece], pos, CheckRotation(x, z));
                mapPiece.transform.SetParent(transform);
                yield return new WaitForSeconds(1);
            }
        }
    }*/
    #endregion
}
