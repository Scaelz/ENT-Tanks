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

    public void Move(float force)
    {
        Vector3 movement = transform.forward * force * speed * Time.deltaTime;
        rb.AddForce(movement);
        //rb.MovePosition(transform.position + movement);
    }

    public void Turn(float input_horizontal, float input_vertical)
    {
        float turn_coef = input_vertical >= 0 ? 1 : -1;
        float turn = input_horizontal * turnSpeed * Time.deltaTime * turn_coef;
        Quaternion q = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * q);
    }

    public void Turn(Quaternion quaternion)
    {
        rb.MoveRotation(quaternion);
    }
}
