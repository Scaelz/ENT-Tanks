using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankHealth), typeof(AudioSource), typeof(TankShoot))]
public class TankEffects : MonoBehaviour
{
    [Header("Visual effects")]
    [SerializeField] GameObject[] explosions;
    TankHealth tankHealth;
    [SerializeField] ParticleSystem shieldSystem;
    [SerializeField] float shiledParticlesCount;
    [Header("Sound effects")]
    [SerializeField] AudioClip[] destructionSounds;
    [SerializeField] AudioClip[] shootingSounds;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        tankHealth = GetComponent<TankHealth>();
        tankHealth.OnGotKilled += PlayRandomExplosion;
        tankHealth.OnShieldStateChanged += ShieldState;
    }

    void PlayRandomExplosion()
    {
        Vector3 point = transform.position;
        GameObject prefab = explosions[Random.Range(0, explosions.Length)];
        ParticleSystem ps = Instantiate(prefab, point, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(ps.gameObject, 5);
    }

    public void ShieldState(bool state)
    {
        if (state)
        {
            shieldSystem.Play();
        }
        else
        {
            shieldSystem.Stop();
        }
    }

    void UpdateShield(float value)
    {
        var emission = shieldSystem.emission;
        emission.rateOverTime = Mathf.Clamp(value, 0, shiledParticlesCount);
    }
}
