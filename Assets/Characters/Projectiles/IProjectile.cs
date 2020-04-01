using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float Power { get; }
    float Speed { get; }
    void SetSpeed(float speed);
}
