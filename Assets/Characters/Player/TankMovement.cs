using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour, IMoveable
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;

    public float Speed => speed;
    public float TurnSpeed => turnSpeed;

    public void MoveTo(Vector3 destination)
    {
        Vector3 movement = destination * speed * Time.deltaTime;
        rb.AddForce(movement);
    }

    public bool IsMoving()
    {
        return true;
    }

    public void Turn(Vector3 direction)
    {
        float turn_coef = direction.z >= 0 ? 1 : -1;
        float turn = direction.x * turnSpeed * Time.deltaTime * turn_coef;
        Quaternion q = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * q);
    }

    public void Turn(float input_horizontal, float input_vertical)
    {
        float turn_coef = input_vertical >= 0 ? 1 : -1;
        float turn = input_horizontal * turnSpeed * Time.deltaTime * turn_coef;
        Quaternion q = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * q);
    }

    public void Turn(Quaternion angle, float turnSpeed)
    {
        rb.MoveRotation(angle);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}
