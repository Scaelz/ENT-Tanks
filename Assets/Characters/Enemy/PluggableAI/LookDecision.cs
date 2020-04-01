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
        Debug.DrawRay(controller.Shooting.Muzzle.position, controller.Shooting.Muzzle.forward * 90, Color.red);
        if(Physics.SphereCast(controller.Shooting.Muzzle.position, rayRadius,
            controller.Shooting.Muzzle.forward, out RaycastHit hit, 1000, layerMask))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.tag == "Player")
                return true;
        }
        return false;
    }
}
