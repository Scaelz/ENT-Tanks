using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] SpawnInfo[] spawnInfo;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] SpawnPosition playerSpawn;
    int[] spawnsMade;
    SpawnPosition[] spawnPositions;

    int totalSpawnCount = 0;
    [SerializeField] float spawnDelay;
    [SerializeField] float spawnFrequency;
    [SerializeField] bool isActive = true;
    [SerializeField] float timer;
    [SerializeField] GameObject spawnEffect;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spawnPositions = FindObjectsOfType<SpawnPosition>();
        spawnsMade = new int[spawnInfo.Length];
        GameController.OnGameStarted += StartSpawn;
    }

    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer > spawnFrequency)
            {
                timer = 0;
                StartCoroutine(Spawn());
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
        int index = Random.Range(0, spawnInfo.Length);
        SpawnInfo info = spawnInfo[index];
        var availableSpawn = spawnPositions.Where(x => x.idle == true && x.AiSpawn == true).ToArray();
        if (availableSpawn.Length == 0)
        {
            yield break;
        }
        SpawnPosition spawn = availableSpawn[Random.Range(0, availableSpawn.Length)];
        spawn.idle = false;
        StartCoroutine(RunVFX(spawn.position, spawnDelay, spawnEffect));
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(info.prefab, spawn.position, Quaternion.identity);
        PlaySound();
        spawnsMade[index]++;
        spawn.idle = true;
        
    }

    void PlaySound()
    {
        audioSource.Play();
    }

    IEnumerator RunVFX(Vector3 spawnPoint, float time, GameObject vfxPrefab)
    {
        GameObject go = Instantiate(vfxPrefab, spawnPoint, Quaternion.identity);
        yield return new WaitForSeconds(time);
        Destroy(go);
    }

    IEnumerator SpawnPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(PlayerPrefab, playerSpawn.position, Quaternion.identity);
        PlaySound();
    }

    public void StartSpawn()
    {
        SpawnPlayer();
        isActive = true;
    }

    void SpawnPlayer()
    {
        StartCoroutine(RunVFX(playerSpawn.position, spawnDelay, spawnEffect));
        StartCoroutine(SpawnPlayer(spawnDelay));
    }


}
