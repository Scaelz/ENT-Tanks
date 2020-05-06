using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBonus : MonoBehaviour
{
    [SerializeField] float timeToDeath;
    [SerializeField] GameObject meshObject;

    void Update()
    {
        timeToDeath -= Time.deltaTime;
        if (timeToDeath <= 0)
        {
            HideBonus();
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        PlayerController playerController = collider.GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            // Player take bonus
            TakeBonus();
            AkSoundEngine.PostEvent("play_bonus", gameObject);
            timeToDeath = 0;
        }
    }

    private void HideBonus()
    {
        if (meshObject != null) meshObject.SetActive(false);
        // GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject, 25);
    }

    public virtual void TakeBonus() { }
}
