using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankProjectile : MonoBehaviour, IProjectile
{
    [Header("Basic properties")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float explodeRadius;
    [SerializeField] LayerMask explodeMask;
    [SerializeField] float speed;
    [SerializeField] float power;
    public float Power => power;
    public float Speed => speed;
    float destructionTimer = 0;
    bool hitRegistered;

    public void SetSpeed(float speed)
    {
        rb.velocity = Vector3.zero;
        this.speed = speed;
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        hitRegistered = false;
        destructionTimer = 0;
    }

    private void Update()
    {
        destructionTimer += Time.deltaTime;
        if(destructionTimer >= 3)
        {
            ProjectilePool.Instance.ReturnToPool(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hitRegistered)
        {
            hitRegistered = true;
            ProjectilePool.Instance.ReturnToPool(gameObject);
            Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius, explodeMask);
            DestructionSystem.DestructPieces(colliders);
            ExplosionsPool.Instance.GetInstance(transform.position, Quaternion.identity).GetComponent<ProjectileDestructionEffects>().RunFX();
        }

    }
}
