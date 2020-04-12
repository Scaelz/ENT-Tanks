using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingSystem : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    protected Queue<GameObject> poolQueue;
    [SerializeField] protected int prewarmCount;

    public GameObject GetInstance(Vector3 position, Quaternion rotation)
    {
        if(poolQueue.Count == 0)
        {
            PreWarm(1);
        }

        GameObject go = poolQueue.Dequeue();
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.SetActive(true);
        return go;
    }

    public void PreWarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(prefab);
            ReturnToPool(newObject);
        }
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        poolQueue.Enqueue(objectToReturn);
    }
}
