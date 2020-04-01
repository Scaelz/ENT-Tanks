using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Search")]
public class SearchDecision : Decision
{
    public float decisionTimer = 1;

    public override bool Decide(AiController controller)
    {
        return SearchForPlayer(controller);
    }

    bool SearchForPlayer(AiController controller)
    {
        if (!controller.Movement.IsMoving())
        {
            if (controller.CheckActionTimer(decisionTimer))
            {
                controller.TrackPlayer(false);
                return true;
            }
        }
        return false;
    }
}
