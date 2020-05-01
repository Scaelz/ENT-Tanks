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
        if(controller.chaseTarget == null)
        {
            return;
        }
        controller.Shooting.Aim(controller.chaseTarget.transform.position);
        controller.Movement.MoveTo(controller.chaseTarget.transform.position);
        if (!controller.Movement.IsMoving())
        {
            controller.Movement.Turn(controller.chaseTarget.transform.position - controller.transform.position);
        }


        //controller.TrackPlayer(true);
        //controller.SavePlayerPosition();
    }
}
