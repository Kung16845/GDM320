using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPolling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Name;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Transform player;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Hp>().transform;
        CreateObjectPooling();
    }
    public void CreateObjectPooling()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>(); //สร้างตัวแปรเก็บค่าเป็น Dictionary

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectsPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.gameObject.transform);
                obj.SetActive(false);
                objectsPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.Name, objectsPool);
        }

    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "doesn't excist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);


        return objectToSpawn;
    }
}
