using System;
using System.Collections.Generic;
using UnityEngine;
public interface IDestructable
{
    float Health { get; }
    event Action<float> OnGotHit;
    event Action OnDestroyed;
    void Hit(float damage);
    void Hit(float damage, Vector3 hitDirection);

    void Hit(List<Collider> colliders, Vector3 hitDirection);
}
