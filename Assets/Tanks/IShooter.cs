using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter
{
    GameObject ProjectilePrefab { get; }
    float Power { get; }
    float ReloadSpeed { get; }
    Transform Muzzle { get; }
    event Action<Vector3, Vector3> OnShoot;
    void Shoot();
    void Aim(Vector3 aimPostion);
    void Reload();
}
