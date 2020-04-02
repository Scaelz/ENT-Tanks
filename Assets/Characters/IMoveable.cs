using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    float Speed { get; }

    void MoveTo(Vector3 destination);
    void StopMoving();
    bool IsMoving();
    void Turn(Vector3 direction);
}
