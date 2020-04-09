using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, IMoveable
{
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    public float Speed => agent.speed;
    [SerializeField] float trunSpeed;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
        agent.isStopped = false;
    }

    public bool IsMoving()
    {
        if(agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            return true;
        }
        return false;
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void Turn(Vector3 direction)
    {
        var rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, trunSpeed);
    }
}
