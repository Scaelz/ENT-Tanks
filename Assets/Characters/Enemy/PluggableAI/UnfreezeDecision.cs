using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Unfreeze")]
public class UnfreezeDecision : Decision
{
    public override bool Decide(AiController controller)
    {
        return Fridge.isOn;
    }
}
