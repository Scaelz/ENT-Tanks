using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    static int enemyLeft;
    public static event Action<int> OnEnemyKilled;
    public static event Action OnAllEnemiesDead;

    private void Start()
    {
        SpawnSystem.OnSpawnsCounted += SetEnemyCount;
    }

    void SetEnemyCount(int count)
    {
        enemyLeft = count;
        OnEnemyKilled?.Invoke(enemyLeft);
    }

    public static void EnemyKilled()
    {
        enemyLeft--;
        OnEnemyKilled?.Invoke(enemyLeft);
        if(enemyLeft <= 0)
        {
            OnAllEnemiesDead?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnEnemyKilled = null;
        OnAllEnemiesDead = null;
    }
}
