using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        
    }

    [SerializeField]
    List<Pool> pools;

    [SerializeField]
    public Dictionary<string, Queue<GameObject>> poolDictionaly;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionaly = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionaly.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 Position, Quaternion rotation)
    {

        if (!poolDictionaly.ContainsKey(tag))
        {
            Debug.Log("네가 찾는 " + tag + "는 없음...");
        }
        GameObject obj = poolDictionaly[tag].Dequeue();

        obj.SetActive(true);
        obj.transform.position = Position;
        obj.transform.rotation = rotation;
        return obj;
    }
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionaly.ContainsKey(tag))
        {
            Debug.Log("네가 찾는 " + tag + "는 없음...");
        }

        obj.SetActive(false);
        poolDictionaly[tag].Enqueue(obj);
    }
}