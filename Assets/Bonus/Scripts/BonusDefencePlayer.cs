using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDefencePlayer : BaseBonus
{
    [SerializeField] float timeImmortality = 15;

    override public void TakeBonus()
    {
        foreach (PlayerController playerController in FindObjectsOfType<PlayerController>())
        {
            TankHealth tankHealth = playerController.gameObject.GetComponent<TankHealth>();
            TankEffects tankEffects = playerController.gameObject.GetComponent<TankEffects>();
            float tmpArmor = tankHealth.Armor;
            StartCoroutine(ActivedShield(tankHealth, tankEffects, tmpArmor, timeImmortality));
        }
        Debug.Log("Defence ON!!!");
    }
    public static IEnumerator ActivedShield(TankHealth tankHealth, TankEffects tankEffects, float tmpArmor, float timeImmortality)
    {
        tankHealth.SetArmor(9999);
        tankEffects.ShieldState(true);
        yield return new WaitForSeconds(timeImmortality);
        tankHealth.SetArmor(tmpArmor);
        tankEffects.ShieldState(false);

    }
}
