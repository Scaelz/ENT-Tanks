using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public bool idle = true;
    public bool AiSpawn = true;
    public Vector3 position;
    [SerializeField] ParticleSystem preparationVFX;
    [SerializeField] ParticleSystem spawnVFX;
    [SerializeField] float lifeTime;
    private void Start()
    {
        position = transform.position;
    }

    public void RunPrepare()
    {
        StopAllCoroutines();
        spawnVFX.Stop();
        preparationVFX.Play();
    }

    public void RunSpawn()
    {
        preparationVFX.Stop();
        spawnVFX.Play();
        StartCoroutine(DelyaedTurnOff(lifeTime));
    }

    IEnumerator DelyaedTurnOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        TurnOff();
    }

    public void TurnOff()
    {
        preparationVFX.Stop();
        spawnVFX.Stop();
        idle = true;
    }
}
