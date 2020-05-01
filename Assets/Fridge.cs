using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    float timer;
    float duration;
    public static bool isOn = false;
    public static event Action<bool> OnFreezeStateChanged;

    private void Awake()
    {
        OnFreezeStateChanged = null;
    }

    private void Update()
    {
        if (isOn)
        {
            timer += Time.deltaTime;
            if(timer >= duration)
            {
                isOn = false;
                timer = 0;
                OnFreezeStateChanged?.Invoke(isOn);
            }
        }
    }

    public void FreezeEnemies(float duration)
    {
        this.duration = duration;
        isOn = true;
        OnFreezeStateChanged?.Invoke(isOn);
    }

    private void OnDestroy()
    {
        OnFreezeStateChanged = null;
    }
}
