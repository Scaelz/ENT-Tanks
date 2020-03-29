using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType
{
    AI,
    Player
}

public interface IController
{
    ControlType TypeOfControl { get; }
    IMoveable Movement { get; }
    IShooter Shooting { get; }
}
