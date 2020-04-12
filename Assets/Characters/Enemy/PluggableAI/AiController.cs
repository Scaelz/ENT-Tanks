using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiController : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] State currentState;
    [SerializeField] State remainState;
    [SerializeField] State freezeState;
    [SerializeField] float actionTime;
    float timer;
    public bool PlayerInFov;

    [SerializeField] IMoveable movement;
    public IMoveable Movement => movement;
    [SerializeField] TankShoot shooting;
    public TankShoot Shooting => shooting;
    bool trackPlayer;
    PlayerController playerController;
    [SerializeField] Transform[] patrolRoute;
    [SerializeField] int patrolIndex;
    Vector3 lastPlayerPosition;

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

    private void Start()
    {
        PlayerInFov = true;
        if (Fridge.isOn)
        {
            Freeze(true);
        }
        Fridge.OnFreezeStateChanged += Freeze;
        movement = GetComponent<IMoveable>();
        playerController = FindObjectOfType<PlayerController>();
        GetRouteMarks();
    }

    void Freeze(bool state)
    {
        if (state)
        {
            TransitionToState(freezeState);
        }
        GetComponent<TankEffects>().FreezeEffect(state);
    }

    public Vector3 GetForward()
    {
        return transform.forward;
    }

    public Vector3 GetLastSeenPosition()
    {
        return lastPlayerPosition;
    }

    public void SavePlayerPosition()
    {
        lastPlayerPosition = playerController.transform.position;
    }

    public void SetActionTime(float time)
    {
        actionTime = time;
    }

    public void TrackPlayer(bool state)
    {
        trackPlayer = state;
    }

    void GetRouteMarks()
    {
        var marks = GameObject.FindGameObjectsWithTag("RouteMark");
        int count = marks.Length;
        patrolRoute = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            patrolRoute[i] = marks[i].transform;
        }
        System.Random rand = new System.Random();
        patrolRoute = patrolRoute.OrderBy(x => rand.Next()).ToArray();
    }

    public int GetRouteLength()
    {
        return patrolRoute.Length;
    }

    public Vector3 GetNextSpot()
    {
        return patrolRoute[patrolIndex].position;
    }

    private void Update()
    {

        if (trackPlayer)
        {
            Shooting.Aim(playerController.transform.position);
        }
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
            OnStateChanged();
        }
    }

    public bool CheckActionTimer(float countDown)
    {
        actionTime = countDown;
        timer += Time.deltaTime;
        return (timer >= actionTime);
    }

    void OnStateChanged()
    {
        timer = 0;
    }

    private void OnDestroy()
    {
        ScoreManager.AddScore(score);
        EnemyCounter.EnemyKilled();
        Fridge.OnFreezeStateChanged -= Freeze;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        PlayerInFov = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        PlayerInFov = false;
    //    }
    //}
}
