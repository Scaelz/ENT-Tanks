using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] SpawnInfo[] spawnInfo;
    int[] spawnsMade;
    SpawnPosition[] spawnPositions;

    int totalSpawnCount = 0;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnFrequency;
    bool isActive = false;
    float timer;

    private void Start()
    {
        spawnPositions = FindObjectsOfType<SpawnPosition>();
        spawnsMade = new int[spawnInfo.Length];
    }

    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer > spawnFrequency)
            {
                timer = 0;
                Spawn();
                UpdateSpawnArray();
            }
        }
    }

    void UpdateSpawnArray()
    {
        List<SpawnInfo> temp = new List<SpawnInfo>();
        for (int i = 0; i < spawnInfo.Length; i++)
        {
            if(spawnInfo[i].count > spawnsMade[i])
            {
                temp.Add(spawnInfo[i]);
            }
        }

        spawnInfo = temp.ToArray();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        int index = Random.Range(0, spawnInfo.Length);
        SpawnInfo info = spawnInfo[index];
        var availableSpawn = spawnPositions.Where(x => x.idle == true).ToArray();
        SpawnPosition spawn = availableSpawn[Random.Range(0, availableSpawn.Length)];
        Instantiate(info.prefab, spawn.position, Quaternion.identity);
        spawnsMade[index]++;
    }

    public void StartSpawn()
    {
        isActive = true;
    }
}
