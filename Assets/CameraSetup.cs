using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public bool CameraSet { get; private set; }

    public void SetupEnded()
    {
        CameraSet = true;
    }
}
