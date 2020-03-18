using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITankMovement
{
    float Speed { get; }
    float TurnSpeed { get; }

    void Turn(Quaternion angle, float turnSpeed);
    void Turn(float verticalInput, float horizontalInput);
    void Move(float inputValue);
    void Stop();
}
