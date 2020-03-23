using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IMoveable), typeof(Rigidbody), typeof(IShooter))]
public class PlayerController : MonoBehaviour, IController
{ 
    Rigidbody rb;
    [SerializeField] ControlType typeOfControl;
    public ControlType TypeOfControl { get => typeOfControl; private set => typeOfControl = value; }

    public IMoveable Movement { get; private set; }

    public IShooter Shooting { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Shooting = GetComponent<IShooter>();
        Movement = GetComponent<IMoveable>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Movement.Move(v);
        Movement.Turn(h, v);
    }

    void AimAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Shooting.Aim(hit.point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Shooting.Shoot();
        }
        AimAtCursor();
    }
}
