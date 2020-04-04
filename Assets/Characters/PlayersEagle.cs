using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersEagle : MonoBehaviour, IKillable
{
    public GameObject GO => gameObject;
    [SerializeField] float health;
    public float Health => health;
    [SerializeField] float armor;
    public float Armor => armor;

    public event Action OnEagleDead;


    public void Damage(float value)
    {
         if (armor > 0)
        {
            value = Math.Abs(armor - value);
        }
        health -= value;
        if(health <= 0)
        {
            KillThis();
        }
    }

    public void KillThis()
    {
        OnEagleDead?.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();
        if(projectile != null)
        {
            Damage(projectile.Power);
        }
    }
}
