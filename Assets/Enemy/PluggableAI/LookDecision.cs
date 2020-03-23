using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public LayerMask layerMask;
    public float rayRadius;

    public override bool Decide(AiController controller)
    {
        bool playerInSight = Look(controller);
        return playerInSight;
    }

    bool Look(AiController controller)
    {
        Debug.DrawRay(controller.transform.position, controller.transform.forward * 90, Color.red);
        if(Physics.SphereCast(controller.transform.position, rayRadius, 
            controller.transform.forward, out RaycastHit hit))
        {
            if(hit.transform.tag == "Player")
                return true;
        }
        return false;
    }
}
