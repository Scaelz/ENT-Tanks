using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestructionEffects : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] float lifeTime;
    float timer;

    private void OnDisable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            ExplosionsPool.Instance.ReturnToPool(gameObject);
        }
    }

    public void RunFX()
    {
        explosionFX.Play();
        Utils.PlayRandomSound(audioSource, audioClips);
    }
}
