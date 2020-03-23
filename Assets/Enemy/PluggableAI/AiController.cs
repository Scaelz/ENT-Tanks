using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    [SerializeField] State currentState;
    [SerializeField] State remainState;


    [SerializeField] EnemyMovement movement;
    public EnemyMovement Movement => movement;
    [SerializeField] TankShoot shooting;
    public TankShoot Shooting => shooting; 

    PlayerController playerController;
    [SerializeField] Transform[] patrolRoute;
    [SerializeField] int patrolIndex;
    public int PatrolIndex
    {
        get
        {
            return patrolIndex;
        }
        set
        {
            if(value >= 0 && value < patrolRoute.Length)
            {
                patrolIndex = value;
            }
        }
    }

    public int GetRouteLength()
    {
        return patrolRoute.Length;
    }

    public Vector3 GetNextSpot()
    {
        return patrolRoute[patrolIndex].position;
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public Vector3 GetTargetPosition()
    {
        if (playerController != null)
        {
            return playerController.transform.position;
        }
        return Vector3.zero;
    }

    public bool IsTargetActive()
    {
        return playerController.isActiveAndEnabled;
    }

    public void TransitionToState(State newState)
    {
        if (newState != remainState)
        {
            currentState = newState;
        }
    }
}
