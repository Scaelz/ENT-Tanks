using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankProjectile : MonoBehaviour, IProjectile
{
    [Header("Basic properties")]
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float power;
    [SerializeField] GameObject blockPickerGO;
    [SerializeField] BlockPicker blockPicker;
    public float Power => power;
    public float Speed => speed;
    [Header("FX")]
    [SerializeField] GameObject[] explosionPrefabs;
    [SerializeField] AudioClip[] explosionSounds;
    [SerializeField] float volume, pitch;

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
        //rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Dictionary<IHitable, List<Collider>> dict = new Dictionary<IHitable, List<Collider>>();
        Vector3 dir = -transform.forward;
        dir = dir.normalized;
        foreach (Collider item in blockPicker.GetDestructables())
        {
            IHitable block = item.gameObject.GetComponentInParent<IHitable>();
            if (dict.ContainsKey(block))
            {
                dict[block].Add(item);
            }
            else
            {
                List<Collider> colliders = new List<Collider>();
                colliders.Add(item);
                dict.Add(block, colliders);
            }
        }
        foreach (KeyValuePair<IHitable, List<Collider>> item in dict)
        {
            item.Key.Hit(item.Value, dir, collision.contacts[0].point);
        }
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        CreateDestructionFX();
        MonoUtils.PlayAudioClip(explosionSounds[Random.Range(0, explosionSounds.Length)], transform.position, volume, pitch);
        Destroy(gameObject);
    }

    void CreateDestructionFX()
    {
        GameObject fx_go = MonoUtils.InstanciateRandom(explosionPrefabs, transform.position, Quaternion.identity);
        Destroy(fx_go, 5);
    }
}
