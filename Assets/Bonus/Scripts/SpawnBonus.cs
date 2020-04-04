using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    [SerializeField] int chanceToSpawn = 30;
    [SerializeField] List<GameObject> bonusObject;
    [SerializeField] int [] bonusChances = {60, 30, 10};

    int totalChance, randomNumber;
    float minX, maxX, minZ, maxZ;

    void Start()
    {
        MinMaxPosition();
        foreach (var item in bonusChances)
        {
            totalChance += item;
        }
    }

    public void OnAddTankHealth(TankHealth tankHealth)
    {
        tankHealth.OnSpawnBonus += SpawnObjectWithChance;
    }
    public void OnRemoveTankHealth(TankHealth tankHealth)
    {
        tankHealth.OnSpawnBonus -= SpawnObjectWithChance;
    }

    private void MinMaxPosition()
    {
        maxX = transform.position.x + transform.localScale.x / 2;
        minX = transform.position.x - transform.localScale.x / 2;
        maxZ = transform.position.z + transform.localScale.z / 2;
        minZ = transform.position.z - transform.localScale.z / 2;
    }

    public void SpawnObjectWithChance()
    {
        randomNumber = Random.Range(0, 100);
        if (randomNumber >= chanceToSpawn) return;
        SpawnBonusWithChance();
    }

    public void SpawnBonusWithChance()
    {
        randomNumber = Random.Range(0, totalChance);
        for (int i = 0; i < bonusChances.Length; i++)
        {
            if (randomNumber <= bonusChances[i])
            {
                Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
                Instantiate(bonusObject[i], spawnPosition, Quaternion.AngleAxis(90f, new Vector3(1,0,0)));
                return;
            }
            else
            {
                randomNumber -= bonusChances[i];
            }
        }
    }

}
