using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPicker : MonoBehaviour
{
    List<IDestructable> destructables;

    private void Start()
    {
        destructables = new List<IDestructable>();
    }

    public List<IDestructable> GetDestructables() => destructables;

    IDestructable GetDestructable(Collider other)
    {
        return other.gameObject.GetComponent<IDestructable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDestructable destructable = GetDestructable(other);
        if (destructable != null)
        {
            if (!destructables.Contains(destructable))
            {
                destructables.Add(destructable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDestructable destructable = GetDestructable(other);
        if (destructable != null)
        {
            if (destructables.Contains(destructable))
            {
                destructables.Remove(destructable);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
    }
}
