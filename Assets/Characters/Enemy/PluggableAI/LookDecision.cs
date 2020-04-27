using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public LayerMask layerMask;
    public float rayRadius;
    public float viewAngle;

    public override bool Decide(AiController controller)
    {
        bool playerInSight = Look(controller);
        return playerInSight;
    }

    bool Look(AiController controller)
    {
        Vector3[] targets = new Vector3[2] { controller.GetPlayerPosition(), controller.GetBunkerPosition() };

        if (controller.PlayerInFov)
        {
            foreach (Vector3 target in targets)
            {
                var inDirection = target - controller.Shooting.Muzzle.position;
                float angle = Vector3.Angle(target - controller.Shooting.Muzzle.position, controller.Shooting.Muzzle.forward);
                if (angle > -(viewAngle / 2) && angle < viewAngle / 2)
                {
                    if (Physics.Raycast(controller.Shooting.Muzzle.position, inDirection, out RaycastHit hit, 999, layerMask))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            controller.chaseTarget = hit.transform.gameObject;
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
