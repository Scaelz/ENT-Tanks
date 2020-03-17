using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPicker : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    int maskValue;
    List<Collider> destructables;

    private void Start()
    {
        maskValue = LayerMask.NameToLayer("BrickParts");
        Debug.Log(maskValue);
        destructables = new List<Collider>();
    }

    public List<Collider> GetDestructables() => destructables;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == maskValue)
        {
            if (!destructables.Contains(other))
            {
                destructables.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == maskValue)
        {
            if (destructables.Contains(other))
            {
                destructables.Remove(other);
            }
        }
    }
}
