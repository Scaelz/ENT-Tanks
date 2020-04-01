using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankHealth))]
public class TankEffects : MonoBehaviour
{
    [Header("Visual effects")]
    [SerializeField] GameObject[] explosions;
    TankHealth tankHealth;

    private void Start()
    {
        tankHealth = GetComponent<TankHealth>();
        tankHealth.OnGotKilled += PlayRandomExplosion;
    }

    void PlayRandomExplosion()
    {
        Vector3 point = transform.position;
        GameObject prefab = explosions[Random.Range(0, explosions.Length)];
        ParticleSystem ps = Instantiate(prefab, point, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(ps.gameObject, 5);
    }

}
