using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Brick : Block, IHitable
{
    [Header("Destruction properties")]
    [SerializeField] float minDestructionForce;
    [SerializeField] float maxDestructionForce;
    [SerializeField] float brokenPartsLifeTime;
    [SerializeField] LayerMask collisionLayer;
    Rigidbody[] rigidbodies;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public override void Hit(List<Collider> colliders, Vector3 hitDirection, Vector3 hitPoint)
    {
        foreach (Collider item in colliders)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.gameObject.layer = Utils.GetLayerMaskInt(collisionLayer);
            SetupExplosion(rb, hitDirection);
            GetRidOfBrokenParts(rb);
        }
        Utils.PlayRandomSound(audioSource, hitAudioClips);
        InstanciateHitEffect(hitPoint);
    }

    void InstanciateHitEffect(Vector3 spawnPoint)
    {
        if (hitEffectsPrefab.Length > 0)
        {
            GameObject vfxPrefab = hitEffectsPrefab[Random.Range(0, hitEffectsPrefab.Length)];
            //Transform spawnTransform = vfxSpawnPoint == null ? spawnPoint : vfxSpawnPoint;
            GameObject vfxGo = Instantiate(vfxPrefab, spawnPoint, Quaternion.identity);
            ParticleSystem particleSystem = vfxGo.GetComponent<ParticleSystem>();
            particleSystem.Play();
        }
    }

    void SetupExplosion(Rigidbody rb, Vector3 explosionDirection)
    {
        float power = Random.Range(minDestructionForce, maxDestructionForce);
        rb.AddForce(explosionDirection * power);  
    }

    void GetRidOfBrokenParts(Rigidbody rb)
    {
        StartCoroutine(Utils.DelayedObjectDisable(rb, brokenPartsLifeTime));
    }
}
