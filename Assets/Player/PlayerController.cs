using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankShoot), typeof(Rigidbody), typeof(TankMovement))]
public class PlayerController : MonoBehaviour
{
    TankShoot shoot;
    TankMovement movement;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        shoot = GetComponent<TankShoot>();
        movement = GetComponent<TankMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        movement.Move(v);
        movement.Turn(h, v);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            shoot.Shoot();
        }
    }
}
