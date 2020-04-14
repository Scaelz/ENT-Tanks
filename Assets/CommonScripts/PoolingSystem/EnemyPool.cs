using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : PoolingSystem
{
    public static EnemyPool Instance;
    public bool isEmpty { get; private set; }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        poolQueue = new Queue<GameObject>();
    }

    public new GameObject GetInstance(Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        if(poolQueue.Count > 0)
        {
            result = base.GetInstance(position, rotation);
        }
        if(poolQueue.Count == 0)
        {
            isEmpty = true;
        }
        return result;
    }
} 
