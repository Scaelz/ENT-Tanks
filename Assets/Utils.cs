using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int GetLayerMaskInt(LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
    }

    public static IEnumerator DelayedObjectDisable <T> (List<T> objectsList, float delay) where T: Component
    {
        yield return new WaitForSeconds(delay);
        foreach (var item in objectsList)
        {
            item.gameObject.SetActive(false);
        }
    }

    public static IEnumerator DelayedObjectDisable<T>(T obj, float delay) where T : Component
    {
        yield return new WaitForSeconds(delay);
        obj.gameObject.SetActive(false);
    }

    public static void PlayRandomSound(AudioSource source, AudioClip[] audioClipsArray, float volume = 1, float pitch = 1)
    {
        if (audioClipsArray.Length > 0)
        {
            AudioClip clip = audioClipsArray[Random.Range(0, audioClipsArray.Length)];
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
    }
}

public class MonoUtils: MonoBehaviour
{
    public static GameObject InstanciateRandom(GameObject[] gameObjects, Vector3 position, Quaternion rotation)
    {
        int index = Random.Range(0, gameObjects.Length);
        return Instantiate(gameObjects[index], position, rotation);
    }
}
