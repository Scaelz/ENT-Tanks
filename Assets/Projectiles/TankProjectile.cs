using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankProjectile : MonoBehaviour, IProjectile
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float power;
    [SerializeField] GameObject blockPickerGO;
    [SerializeField] BlockPicker blockPicker;
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
        if (collision.gameObject.GetComponent<IDestructable>() != null)
        {
            foreach (IDestructable item in blockPicker.GetDestructables())
            {
                item.Hit(Power);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDestructable>() != null)
        {
            blockPickerGO.SetActive(true);
            Transform target = blockPicker.transform;
            target.position = transform.position;
            //Vector3 scale = new Vector3(other.transform.localScale.x / 2, other.transform.localScale.y / 2, other.transform.localScale.z / 4);
            //Collider[] colliders = Physics.OverlapBox(other.transform.position, scale, Quaternion.identity);

            //foreach (Collider item in colliders)
            //{
            //    Destroy(item.gameObject);
            //}

            foreach (IDestructable item in blockPicker.GetDestructables())
            {
                item.Hit(Power);
            }
            //Destroy(gameObject);
        }
    }
}
