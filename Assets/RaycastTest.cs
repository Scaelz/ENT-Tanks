using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public LayerMask mask;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 90, Color.red);
        if(Physics.SphereCast(transform.position, 1, transform.forward, out RaycastHit hit, 100, mask))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
