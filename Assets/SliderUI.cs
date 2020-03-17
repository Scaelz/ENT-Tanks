using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] string volumeName;

    private void Start()
    {
        if (PlayerPrefs.HasKey(volumeName)) SetVolume(PlayerPrefs.GetFloat(volumeName));
        /*
        else
        {
            Debug.Log(volumeName);
            Debug.Log("no prefs");
            SetVolume(0.8f);
        }
        */
    }

    public string GetVolumeName() => volumeName;

    public void SetVolume(float value)
    {
        gameObject.GetComponent<Slider>().value = value;
        PlayerPrefs.SetFloat(volumeName, value);
        PlayerPrefs.Save();
    }
}
