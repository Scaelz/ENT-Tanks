using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : AiAction
{
    bool leftTurn = true;
    public float turnAngle = 45;
    public override void Act(AiController controller)
    {
        Patrol(controller);
    }

    void Patrol(AiController controller)
    {
        //controller.Movement.Agent.stoppingDistance = 0;
        controller.Movement.MoveTo(controller.GetNextSpot());
        //controller.Shooting.ResetMuzzle();
        LookAround(controller);
        if (!controller.Movement.IsMoving())
        {
            controller.PatrolIndex = (controller.PatrolIndex + 1) % controller.GetRouteLength();
        }
    }

   void LookAround(AiController controller)
    {

        var direction = Quaternion.AngleAxis(-turnAngle, controller.Shooting.Muzzle.up) * controller.GetForward();
        if (!leftTurn)
        {
            direction = Quaternion.AngleAxis(turnAngle, controller.Shooting.Muzzle.up) * controller.GetForward();
        }
        controller.Shooting.AimInDirection(direction, 10);

        if (Vector3.Dot(controller.Shooting.GetMuzzleDirection(), direction) > 0.99f)
        {
            leftTurn = !leftTurn;
        }
    }
}
