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
    }

    public string GetVolumeName() => volumeName;

    public void SetVolume(float value)
    {
        gameObject.GetComponent<Slider>().value = value;
        PlayerPrefs.SetFloat(volumeName, value);
        PlayerPrefs.Save();
    }
}
