using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour, IHitable
{
    [Space(10)]
    [Header("VFX properties")]
    [SerializeField] protected GameObject[] hitEffectsPrefab;
    [SerializeField] protected Transform vfxSpawnPoint;

    public event Action OnGotHit;

    public virtual void Hit(List<Collider> colliders, Vector3 hitDirection, Vector3 hitPoint)
    {
        throw new NotImplementedException();
    }
}
