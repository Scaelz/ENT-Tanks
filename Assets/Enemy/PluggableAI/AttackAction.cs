using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="PluggableAI/Actions/Attack")]
public class AttackAction : AiAction
{
    public LayerMask layerMask;
    public float rayRadius;

    public override void Act(AiController controller)
    {
        Attack(controller);
    }

    void Attack(AiController controller)
    {
        if (controller.CheckActionTimer())
        {
            Debug.DrawRay(controller.transform.position, controller.Shooting.GetMuzzleDirection() * 90, Color.green);
            if (Physics.SphereCast(controller.transform.position, rayRadius,
                controller.Shooting.GetMuzzleDirection(), out RaycastHit hit, layerMask))
            {
                controller.Shooting.Shoot();
            }
        }
    }
}
