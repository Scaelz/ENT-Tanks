using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestructionSystem : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] GameObject[] vfxPrefabs;
    [SerializeField] LayerMask brokenPiecesMask;
    [SerializeField] float decayTime;
    public static DestructionSystem Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public static void DestructPieces(Collider[] colliders)
    {
        Vector3 center = Vector3.zero;
        foreach (Collider cll in colliders)
        {
            NavMeshObstacle obsticle = cll.GetComponent<NavMeshObstacle>();
            obsticle.enabled = false;
            center += cll.transform.position;
            Rigidbody rigidbody = cll.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.gameObject.layer = Utils.GetLayerMaskInt(Instance.brokenPiecesMask);
            Instance.StartCoroutine(Instance.DecayPieces(rigidbody.gameObject, Instance.decayTime));
        }
        center /= colliders.Length;
        Debug.Log($"Center {center}");
        GameObject go = DestructionFXPool.Instance.GetInstance(center, Quaternion.identity);
        go.GetComponent<ParticleSystem>().Play();
        Instance.StartCoroutine(Instance.DecayPieces(go, Instance.decayTime * 2));
    }

    IEnumerator DecayPieces(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
