using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlock : MonoBehaviour, IDestructable
{
    [SerializeField] float health;
    public float Health { get => health; }

    public event Action<float> OnGotHit;
    public event Action OnDestroyed;

    public void Hit(float damage)
    {
        health -= damage;
        OnGotHit?.Invoke(damage);
        //meshDestroy.DestroyMesh();
        if(health <= 0)
        {
            DestroyBlock();
        }
    }

    void DestroyBlock()
    {
        OnDestroyed?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IProjectile projectile = other.GetComponent<IProjectile>();
        if(projectile != null)
        {
            Hit(projectile.Power);
        }
    }
}
