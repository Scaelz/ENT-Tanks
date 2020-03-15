using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.freezeRotation = true;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            v = 0;
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            h = 0;
        }

        if (new Vector3(h, 0, v) != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(h, 0, v) * speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }

        rb.AddForce(new Vector3(h, 0, v) * speed * Time.deltaTime);
        //rb.AddForce(new Vector3(h, 0, v) * speed);
        //if (rb.velocity != Vector3.zero)
        //    transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
}
