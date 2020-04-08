using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFreezeTime : BaseBonus
{
    override public void TakeBonus()
    {
        FindObjectOfType<Fridge>().FreezeEnemies(5);
    }
}
