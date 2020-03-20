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
}
