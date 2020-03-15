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
        //rb.freezeRotation = true;

        //if (Input.GetAxisRaw("Horizontal") != 0)
        //{
        //    v = 0;
        //}
        //else if (Input.GetAxisRaw("Vertical") != 0)
        //{
        //    h = 0;
        //}

        //if (new Vector3(h, 0, v) != Vector3.zero)
        //{
        //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(h, 0, v) * speed * Time.deltaTime);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        //}

        //rb.AddForce(new Vector3(h, 0, v) * speed * Time.deltaTime);
        Vector3 movement = transform.forward * v * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        float turn_coef = v >= 0 ? 1 : -1;
        float turn = h * turnSpeed * Time.deltaTime * turn_coef;
        Quaternion q = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * q);

    }
}
