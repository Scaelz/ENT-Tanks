using System;
using System.Collections.Generic;
using UnityEngine;
public interface IHitable
{
    event Action OnGotHit;
    void Hit(List<Collider> colliders, Vector3 hitDirection, Vector3 hitPoint);
}
