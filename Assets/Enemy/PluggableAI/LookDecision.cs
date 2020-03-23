using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public LayerMask layerMask;
    public float castRadius;

    public override bool Decide(AiController controller)
    {
        bool playerInSight = Look(controller);
        return playerInSight;
    }

    bool Look(AiController controller)
    {
        if(Physics.SphereCast(controller.transform.position, castRadius, 
            controller.transform.forward, out RaycastHit hit, layerMask))
        {
            return true;
        }
        return false;
    }
}
