using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public void SetVolume(float value = 0.8f)
    {
        gameObject.GetComponent<Slider>().value = value;
    }
}
