using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusExplosion : BaseBonus
{
    SpawnBonus spawnBonus;
    TankHealth tankHealth;

    private void Start()
    {
        spawnBonus = FindObjectOfType<SpawnBonus>();
    }

    override public void TakeBonus()
    {
        foreach (EnemyMovement enemyMovement in FindObjectsOfType<EnemyMovement>())
        {
            tankHealth = enemyMovement.gameObject.GetComponent<TankHealth>();
            spawnBonus.OnRemoveTankHealth(tankHealth);
            tankHealth.KillThis();
        }
        Debug.Log("EXPLOSION!!!!");
    }
}
