using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Pooler : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject[] prefabs;
		public int size;
	}

	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;


    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int p = 0; p < pool.prefabs.Length; p++)
            {
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefabs[p]);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
            }
			poolDictionary.Add(pool.tag, objectPool);
		}
    }

	public GameObject SpawnRandomFromPoolIfInnactive(string tag, Vector3 pos, Quaternion rot)
	{
        // checking if the pool i want to take from exists
		if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " does not exist");
            return null;
        }

        // if gameobject innactive, set to next available and break loop
        // if is active, requeu and check next

        GameObject objToSpawn = ReturnRandomPoolObject(tag);
        if (objToSpawn == null) return objToSpawn;

        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rot;
        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }

    #region not finished, not currently working
    GameObject ReturnRandomPoolObject(string tag)
    {
        int objsInPool = poolDictionary[tag].Count;
        List<int> possibleIndex = new List<int>();
        for (int i = 0; i < objsInPool; i++) possibleIndex.Add(i);

        GameObject nextAvailable = null;

        for (int o = 0; o < objsInPool; o++)
        {
            int i = Random.Range(0, poolDictionary[tag].Count);
            GameObject nextObj = poolDictionary[tag].Dequeue(); // need to find a way to do this at a random point (i)
            if (nextObj.activeSelf == false)
            {
                nextObj = nextAvailable;
                Debug.Log(i);
                break;
            }
            else
            {
                possibleIndex.Remove(i);
                o++;
                if (possibleIndex.Count <= 0) ReturnRandomPoolObject(tag);
            }
        }

        return nextAvailable;

        /*GameObject nextObj = poolDictionary[tag].Dequeue();
        if (nextObj.activeSelf == true)
        {
            poolDictionary[tag].Enqueue(nextObj);
            i++;
            if (i > poolDictionary[tag].Count) nextAvailable = null;
        }
        else
        {
            nextAvailable = nextObj;
            return nextAvailable;
        }*/
    }
    #endregion

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " does not exist");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rot;
        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }
    public GameObject SpawnFromPoolIfInnactive(string tag, Vector3 pos, Quaternion rot)
    {
        // checking if the pool i want to take from exists
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + " does not exist");
            return null;
        }

        // if gameobject innactive, set to next available and break loop
        // if is active, requeu and check next
        GameObject nextAvailable = null;
        for (int i = 0; i < poolDictionary.Count;)
        {
            GameObject nextObj = poolDictionary[tag].Dequeue();

            if (nextObj.activeSelf == false)
            {
                nextAvailable = nextObj;
                break;
            }
            else
            {
                poolDictionary[tag].Enqueue(nextObj);
                i++;
                if (i > poolDictionary[tag].Count) nextAvailable = null;
            }
        }

        GameObject objToSpawn = nextAvailable;
        if (objToSpawn == null) return objToSpawn;

        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rot;
        objToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }
}
