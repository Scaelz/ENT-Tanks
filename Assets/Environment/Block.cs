using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Block : MonoBehaviour, IHitable
{
    [Header("Sound properties")]
    [SerializeField] protected AudioClip[] hitAudioClips;
    [Space(10)]
    [Header("VFX properties")]
    [SerializeField] protected GameObject[] hitEffectsPrefab;
    [SerializeField] protected Transform vfxSpawnPoint;
    protected AudioSource audioSource;

    public event Action OnGotHit;

    public virtual void Hit(List<Collider> colliders, Vector3 hitDirection, Vector3 hitPoint)
    {
        throw new NotImplementedException();
    }
}
