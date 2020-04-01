using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Search")]
public class SearchAction : AiAction
{
    public float countDown = 0;

    public override void Act(AiController controller)
    {
        Search(controller);
    }

    void Search(AiController controller)
    {
        controller.Movement.Agent.stoppingDistance = 0;
        Vector3 lastSeenAt = controller.GetLastSeenPosition();
        controller.Movement.MoveTo(lastSeenAt);
        if (!controller.Movement.IsMoving())
        {
            controller.Shooting.Aim(controller.GetTargetPosition());
        }
    }
}
