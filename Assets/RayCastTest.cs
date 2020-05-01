using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform cubeTransform;
    void Update()
    {
        if(Physics.Raycast(transform.position, cubeTransform.position, out RaycastHit hit, 100, layerMask)){
            Debug.Log(hit.transform.name);
        }
    }
}
