using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Active")]
public class ActiveDecision : Decision
{
    public override bool Decide(AiController controller)
    {
        return controller.IsTargetActive();
    }
}
