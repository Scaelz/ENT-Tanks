using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankHealth), typeof(AudioSource))]
public class TankEffects : MonoBehaviour
{
    [Header("Visual effects")]
    [SerializeField] GameObject[] explosions;
    TankHealth tankHealth;
    TankShoot tankShoot;
    [SerializeField] ParticleSystem shieldSystem;
    [SerializeField] float shiledParticlesCount;
    [Header("Sound effects")]
    float volume = 1;
    [SerializeField] AudioClip[] destructionSounds;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip[] shootingSounds;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        tankHealth = GetComponent<TankHealth>();
        tankShoot = GetComponent<TankShoot>();
        tankHealth.OnGotKilled += PlayRandomExplosion;
        tankHealth.OnShieldStateChanged += ShieldState;
        tankHealth.OnGotHit += HitHandler;
        tankHealth.OnGotKilled += DestructionHandler;
        tankShoot.OnShoot += ShootHandler;
    }

    void ShootHandler(Vector3 dir, Vector3 from)
    {
        Utils.PlayRandomSound(audioSource, shootingSounds);
    }

    void PlayRandomExplosion()
    {
        Vector3 point = transform.position;
        GameObject prefab = explosions[Random.Range(0, explosions.Length)];
        ParticleSystem ps = Instantiate(prefab, point, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(ps.gameObject, 5);
    }

    void DestructionHandler()
    {
        AudioClip clip = destructionSounds[Random.Range(0, destructionSounds.Length)];
        MonoUtils.PlayAudioClip(clip, transform.position, volume);
    }

    void HitHandler(float value)
    {
        Utils.PlayRandomSound(audioSource, hitSounds, volume);
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
