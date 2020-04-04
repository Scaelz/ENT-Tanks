using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public bool idle;
    public Vector3 position;

    private void Start()
    {
        position = transform.position;
    }
}
