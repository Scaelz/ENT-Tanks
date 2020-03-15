using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] int maxIndex;
    //[SerializeField] public AudioSource audioSource;

    AudioSource changeMenuSFX;
    [SerializeField] bool isAlive;

    public int GetIndex() => index;
    public int GetMaxIndex() => maxIndex;
    public bool IsAlive() => isAlive;

    public void SetIndex(int value) => index = value;
    public void SetIsAlive(bool value) => isAlive = value;

    public void PlaySound(AudioSource audioSource) => audioSource.Play();

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        changeMenuSFX = FindObjectOfType<SFX>().GetChangeMenuSFX();
    }

    private void Update()
    {
        if (isAlive)
        {
            SelectMenuButton();
        }
    }

    private void SelectMenuButton()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlaySound(changeMenuSFX);
            if (index < maxIndex) index++;
            else index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlaySound(changeMenuSFX);
            if (index > 0) index--;
            else index = maxIndex;
        }
    }
}
