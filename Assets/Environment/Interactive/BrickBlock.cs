using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlock : MonoBehaviour, IDestructable
{
    [SerializeField] float health;
    [SerializeField] GameObject destroyedVersion;
    MeshRenderer renderer;
    Collider cll;
    public float Health { get => health; }

    public event Action<float> OnGotHit;
    public event Action OnDestroyed;


    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        cll = GetComponent<Collider>();
    }

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
        renderer.enabled = false;
        cll.enabled = false;
        destroyedVersion.SetActive(true);
        //gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //IProjectile projectile = other.GetComponent<IProjectile>();
        //if(projectile != null)
        //{
        //    Hit(projectile.Power);
        //}
    }
}
