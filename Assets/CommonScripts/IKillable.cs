using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    GameObject GO { get; }
    float Health { get; }
    float Armor { get; }

    void Damage(float value);
    void KillThis();
}
