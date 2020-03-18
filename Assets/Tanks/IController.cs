using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Controller
{
    AI,
    Player
}

public interface IController
{
    Controller Controller { get; }

}
