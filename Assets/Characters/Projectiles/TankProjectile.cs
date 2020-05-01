using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius, explodeMask);
            Vector3 capsuleTopPoint = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            Vector3 capsuleBotPoint = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
            var colliders = Physics.OverlapCapsule(capsuleBotPoint, capsuleTopPoint, explodeRadius, explodeMask);
            if (colliders.Length > 0)
            {
                DestructionSystem.DestructPieces(colliders);
            }
            
        }
        ExplosionsPool.Instance.GetInstance(transform.position, Quaternion.identity).GetComponent<ProjectileDestructionEffects>().RunFX();
        ProjectilePool.Instance.ReturnToPool(gameObject);
        hitRegistered = false;
    }
}
