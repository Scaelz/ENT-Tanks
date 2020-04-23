using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsPool : PoolingSystem
{
    public static ExplosionsPool Instance;

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
        PreWarm(prewarmCount);
    }
}
