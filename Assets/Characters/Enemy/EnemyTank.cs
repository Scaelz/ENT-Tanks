using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRadars
{
    public string Name { get; private set; }
    public Vector3 Direction { get; private set; }
    public Transform[] Radars { get; private set; }

    public TankRadars(string name, Vector3 dir, Transform[] transforms)
    {
        Name = name;
        Direction = dir;
        Radars = transforms;
    }
}


[RequireComponent(typeof(TankShoot), typeof(Rigidbody), typeof(TankMovement))]
public class EnemyTank : MonoBehaviour
{

    TankShoot shoot;
    TankMovement movement;
    Rigidbody rb;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float obsticleDistance;
    [SerializeField] Transform[] frontObsticleRadars;
    [SerializeField] Transform[] leftSideObsticleRadars;
    [SerializeField] Transform[] rightSideObsticleRadars;
    [SerializeField] Transform[] rearObsticleRadars;
    [SerializeField] float turnTime;
    float turnTimer;
    TankRadars[] tankRadars;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRadars();
        shoot = GetComponent<TankShoot>();
        movement = GetComponent<TankMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void UpdateRadars()
    {
        tankRadars = new TankRadars[]
        {
            //new TankRadars(transform.forward, frontObsticleRadars),
            new TankRadars("rear", -transform.forward, rearObsticleRadars),
            new TankRadars("right", transform.right, rightSideObsticleRadars),
            new TankRadars("left", -transform.right, leftSideObsticleRadars),
        };
    }

    // Update is called once per frame
    void Update()
    {
        shoot.Shoot();
        turnTimer += Time.deltaTime;
        if(turnTimer >= turnTime)
        {
            turnTimer = 0;
            RotateTo(ChoosePath());
        }
    }

    private void FixedUpdate()
    {
        if (!CheckForObsticle(transform.forward, frontObsticleRadars))
        {
            //movement.Move(1);
        }
        else
        {
            RotateTo(ChoosePath());
        }
    }

    void DrawRays(Vector3 direction, Transform[] obsticleRadars)
    {

        foreach (Transform radar in obsticleRadars)
        {
            Debug.DrawRay(radar.position, direction * obsticleDistance, Color.red);
        }
    }

    bool CheckForObsticle(Vector3 direction, Transform[] obsticleRadars)
    {
        foreach (Transform radar in obsticleRadars)
        {
            if (Physics.Raycast(radar.position, direction, out RaycastHit hit, obsticleDistance, layerMask.value))
            {
                return true;
            }
        }
        return false;
    }

    void Shuffle<T>(IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    Vector3 ChoosePath()
    {
        List<Vector3> availableDirection = new List<Vector3>();
        foreach (TankRadars radars in tankRadars)
        {
            if (!CheckForObsticle(radars.Direction, radars.Radars))
            {
                availableDirection.Add(radars.Direction);
            }
        }

        return availableDirection[Random.Range(0, availableDirection.Count)];
    }

    void RotateTo(Vector3 direction)
    {
        rb.transform.rotation = Quaternion.LookRotation(direction, transform.up);
        UpdateRadars();//movement.Turn(Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + 180, 0)));
    }
}
