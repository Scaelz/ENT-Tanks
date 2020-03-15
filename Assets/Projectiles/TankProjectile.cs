using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankProjectile : MonoBehaviour, IProjectile
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float power;
    public float Power => power;
    public float Speed => speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Debug.Log(other.name);
            Destroy(gameObject);
        }
    }
}
