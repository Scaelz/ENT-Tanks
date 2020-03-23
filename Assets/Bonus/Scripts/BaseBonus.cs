using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBonus : MonoBehaviour
{
    [SerializeField] float timeToDeath;

    void Update()
    {
        timeToDeath -= Time.deltaTime;
        if (timeToDeath <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        PlayerController playerController = collider.GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            // Player take bonus
            TakeBonus();
            Destroy(gameObject);
        }
    }
    public virtual void TakeBonus() { }
}
