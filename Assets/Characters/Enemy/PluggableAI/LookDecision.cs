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

    //bool Look(AiController controller)
    //{
    //    Debug.DrawRay(controller.Shooting.Muzzle.position, controller.Shooting.Muzzle.forward * 90, Color.red);
    //    if(Physics.SphereCast(controller.Shooting.Muzzle.position, rayRadius,
    //        controller.Shooting.Muzzle.forward, out RaycastHit hit, 1000, layerMask))
    //    {
    //        if(hit.transform.tag == "Player")
    //            return true;
    //    }
    //    return false;
    //}

    bool Look(AiController controller)
    {
        Debug.DrawRay(controller.Shooting.Muzzle.position, controller.Shooting.Muzzle.forward * 90, Color.blue);
        var direction = Quaternion.AngleAxis(45, controller.Shooting.Muzzle.up) * controller.Shooting.Muzzle.forward;
        Debug.DrawRay(controller.Shooting.Muzzle.position, direction * 90, Color.red);
        direction = Quaternion.AngleAxis(-45, controller.Shooting.Muzzle.up) * controller.Shooting.Muzzle.forward;
        Debug.DrawRay(controller.Shooting.Muzzle.position, direction * 90, Color.red);
       
        if (controller.PlayerInFov)
        {
            float angle = Vector3.Angle(controller.GetTargetPosition() - controller.Shooting.Muzzle.position, controller.Shooting.Muzzle.forward);
            if (angle > -(viewAngle / 2) && angle < viewAngle / 2)
            {
                return true;
            }
        }
        return false;
    }

    void LookAround(AiController controller)
    {

    }
}
