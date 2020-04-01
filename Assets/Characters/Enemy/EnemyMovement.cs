using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
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
}
