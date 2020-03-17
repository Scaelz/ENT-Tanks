using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class BrickBlock : MonoBehaviour, IDestructable
{
    [SerializeField] float health;
    [SerializeField] float minDestrPower, maxDestrPower;
    Rigidbody[] rigidbodies;

    public float Health { get => health; }

    public event Action<float> OnGotHit;
    public event Action OnDestroyed;


    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void Hit(float damage)
    {
        health -= damage;
        OnGotHit?.Invoke(damage);
        if(health <= 0)
        {
            //DestroyBlock();
        }
    }

    public void Hit(float damage, Vector3 hitDirection)
    {
        health -= damage;
        OnGotHit?.Invoke(damage);
        if (health <= 0)
        {
            DestroyBlock(hitDirection);
        }
    }

    public void Hit(List<Collider> colliders, Vector3 hitDirection)
    {
        foreach (Collider item in colliders)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.gameObject.layer = LayerMask.NameToLayer("BrokenParts");
            rb.AddForce(hitDirection * Random.Range(minDestrPower, maxDestrPower));
            Destroy(rb.gameObject, 1.5f);
        }
    }

    void DestroyBlock(Vector3 hitDirection)
    {
        OnDestroyed?.Invoke();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        gameObject.layer = LayerMask.NameToLayer("BrokenParts");
        rb.AddForce(hitDirection * Random.Range(minDestrPower, maxDestrPower));
        Destroy(gameObject, 1.5f);
    }
}
