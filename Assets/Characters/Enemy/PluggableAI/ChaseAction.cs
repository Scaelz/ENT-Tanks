using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : AiAction
{
    public float chaseDistance = 1.2f; 
    public override void Act(AiController controller)
    {
        Chase(controller);
    }

    void Chase(AiController controller)
    {
        //controller.Movement.Agent.stoppingDistance = chaseDistance;
        controller.Movement.MoveTo(controller.GetTargetPosition());
        controller.TrackPlayer(true);
        controller.SavePlayerPosition();
    }
}
