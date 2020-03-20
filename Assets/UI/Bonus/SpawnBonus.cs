using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    [SerializeField] int chanceToSpawn = 30;
    [SerializeField] List<GameObject> bonusObjects;
    [SerializeField] int [] bonusChances = {60, 30, 10};

    int totalChance;
    float minX, maxX, minZ, maxZ;

    void Start()
    {
        MinMaxPosition();
        foreach (var item in bonusChances)
        {
            totalChance += item;
        }
    }

    void Update()
    {
        SpawnObjectWithChance();
    }

    private void SpawnObjectWithChance()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int randomNumber = Random.Range(0, 100);
            if (randomNumber >= chanceToSpawn) return;

            randomNumber = Random.Range(0, totalChance);
            for (int i = 0; i < bonusChances.Length; i++)
            {
                if (randomNumber <= bonusChances[i])
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
                    Instantiate(bonusObjects[i], spawnPosition, Quaternion.identity);
                    return;
                }
                else
                {
                    randomNumber -= bonusChances[i];
                }
            }
        }
    }

    private void MinMaxPosition()
    {
        maxX = transform.position.x + transform.localScale.x / 2;
        minX = transform.position.x - transform.localScale.x / 2;
        maxZ = transform.position.z + transform.localScale.z / 2;
        minZ = transform.position.z - transform.localScale.z / 2;
    }
}
