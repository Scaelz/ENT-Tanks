using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLife : BaseBonus
{
    [SerializeField] int maxHealth = 20;
    override public void TakeBonus()
    {
        foreach (PlayerController playerController in FindObjectsOfType<PlayerController>())
        {
            TankHealth tankHealth = playerController.gameObject.GetComponent<TankHealth>();
            tankHealth.SetHealth(maxHealth);
        }
        Debug.Log("Add Life!!!");
    }
}
