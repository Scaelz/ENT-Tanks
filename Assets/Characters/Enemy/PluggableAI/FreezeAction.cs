using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Freeze")]
public class FreezeAction : AiAction
{
    public override void Act(AiController controller)
    {
        controller.Movement.StopMoving();
    }
}
