using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : AiAction
{
    public override void Act(AiController controller)
    {
        Chase(controller);
    }

    void Chase(AiController controller)
    {
        controller.Movement.MoveTo(controller.GetTargetPosition());
        controller.TrackPlayer(true);
    }
}
