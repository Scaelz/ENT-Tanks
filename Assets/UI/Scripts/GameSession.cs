using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject optionMenu;

    MenuButtonController menuButtonController;
    bool isOptions = false;
    float waitTime = 0.4f;

    private void Start()
    {
        if (optionMenu != null)
        {
            menuButtonController = optionMenu.GetComponent<MenuButtonController>();
            isOptions = menuButtonController.IsAlive();
        }
    }
    void Update()
    {
        HideOptionsMenu();
    }

    private void HideOptionsMenu()
    {
        if (optionMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isOptions = menuButtonController.IsAlive();
                if (isOptions)
                {
                    isOptions = false;
                    menuButtonController.SetIndex(menuButtonController.GetMaxIndex());
                }
                else
                {
                    isOptions = true;
                }
                optionMenu.SetActive(isOptions);
                menuButtonController.SetIsAlive(isOptions);
            }
            if (isOptions && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
                && menuButtonController.GetIndex() == menuButtonController.GetMaxIndex())
            {
                StartCoroutine(WaitAndSet());
            }
        }
    }

    IEnumerator WaitAndSet()
    {
        yield return new WaitForSeconds(waitTime);
        menuButtonController.SetIndex(menuButtonController.GetMaxIndex());
    }
    public void SetVolume(float volume) => CheckVolume("volume", volume);
    public void SetMusicVolume(float volume) => CheckVolume("music", volume);
    public void SetFXVolume(float volume) => CheckVolume("FX", volume);

    private void CheckVolume(string mixerChannel, float volume)
    {
        audioMixer.SetFloat(mixerChannel, Mathf.Lerp(-80, 0, Mathf.Pow(volume, 0.25f)));
    }

    public void SetDefaultVolume()
    {
        SetVolume(0.8f);
        SetMusicVolume(0.8f);
        SetFXVolume(0.8f);

        SliderUI[] sliderUIs = FindObjectsOfType<SliderUI>();
        foreach (SliderUI slider in sliderUIs)
        {
            slider.SetVolume();
        }
    }
}
