using UnityEngine;

public class Bonus : MonoBehaviour
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
            switch (gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "SpeedUpgradeShadow":
                    Debug.Log("SpeedUpgradeShadow");
                    break;
                case "HappyCoffee":
                    Debug.Log("HappyCoffee");
                    break;
                case "angry":
                    Debug.Log("angry");
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
