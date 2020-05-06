using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] AudioSource audioSource;
    public AK.Wwise.Event play_spawn;
    public Vector3 position { get => transform.position; }
    public bool Idle { get; private set; } = true;

    public void Process(float spawnTime)
    {
        Idle = false;
        spawnEffect.Play();
        play_spawn.Post(gameObject);

        StartCoroutine(StopProcessing(spawnTime));
    }

    IEnumerator StopProcessing(float delay)
    {
        yield return new WaitForSeconds(delay);
        spawnEffect.Stop();
        audioSource.Play();

        float clipDuration = audioSource.clip.length;
        yield return new WaitForSeconds(clipDuration);
        Idle = true;
    }
}
