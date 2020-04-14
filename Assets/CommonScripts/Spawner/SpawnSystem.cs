using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] float spawnFrequency;
    [SerializeField] float spawnDuration;
    [SerializeField] bool timeSpawn = true; 
    float timer;  
    [SerializeField] Spawn[] spawns;
    Spawn freeSpawn;
    public SpawnInfo[] spawnInfos;
    public static event Action<int> OnSpawnsCounted;

    private void Start()
    {
        PreWarmAllSpawns();
    }

    private void Update()
    {
        if (timeSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnFrequency)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        if (!EnemyPool.Instance.isEmpty)
        {
            StartCoroutine(CalculateFreeSpawn());
            if (freeSpawn == null)
            {
                return;
            }
            freeSpawn.Process(spawnDuration);
            StartCoroutine(DelayedInstanciate());
            timer = 0;
        }
    }

    IEnumerator DelayedInstanciate()
    {
        yield return new WaitForSeconds(spawnDuration);
        EnemyPool.Instance.GetInstance(freeSpawn.position, Quaternion.identity);
    }
    
    IEnumerator CalculateFreeSpawn()
    {
        freeSpawn = null;
        while (true)
        {
            Spawn spawn = spawns[Random.Range(0, spawns.Length)];
            if(spawn.Idle == true)
            {
                freeSpawn = spawn;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    void PreWarmAllSpawns()
    {
        int total_count = 0;
        List<GameObject> prewarmList = new List<GameObject>();
        foreach (var item in spawnInfos)
        {
            total_count += item.count;
            for (int i = 0; i < item.count; i++)
            {
                prewarmList.Add(item.prefab);
            }
        }
        RandomizePrewarm(prewarmList);

        foreach (var item in prewarmList)
        {
            EnemyPool.Instance.PreWarm(1, item);
        }
        OnSpawnsCounted?.Invoke(total_count);
    }

    void RandomizePrewarm(List<GameObject> listToShuffle)
    {
        System.Random rng = new System.Random();
        int n = listToShuffle.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[n];
            listToShuffle[n] = value;
        }
    }

    private void OnDestroy()
    {
        OnSpawnsCounted = null;
    }
}
