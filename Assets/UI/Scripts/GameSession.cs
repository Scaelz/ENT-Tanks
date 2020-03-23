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

    float masterVolume = 0.8f;
    float musicVolume = 0.8f;
    float FXVolume = 0.8f;

    private void Start()
    {
        LoadVolume();
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
            if (isOptions) Time.timeScale = 0f; else Time.timeScale = 1f;
        }
    }

    IEnumerator WaitAndSet()
    {
        yield return new WaitForSeconds(waitTime);
        menuButtonController.SetIndex(menuButtonController.GetMaxIndex());
    }

    #region Volume Settings
    public void SetMasterVolume(float volume) => SetMixerVolume("volume", volume);
    public void SetMusicVolume(float volume) => SetMixerVolume("music", volume);
    public void SetFXVolume(float volume) => SetMixerVolume("FX", volume);

    private void SetMixerVolume(string mixerChannel, float volume)
    {
        audioMixer.SetFloat(mixerChannel, Mathf.Lerp(-80, 0, Mathf.Pow(volume, 0.25f)));
        SaveVolume(mixerChannel, volume);
    }
    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            masterVolume = PlayerPrefs.GetFloat("volume");
            musicVolume = PlayerPrefs.GetFloat("music");
            FXVolume = PlayerPrefs.GetFloat("FX");

            SetMasterVolume(masterVolume);
            SetMusicVolume(musicVolume);
            SetFXVolume(FXVolume);
        }
        else
        {
            SetDefaultVolume();
        }
        SetValueSlider();
    }
    
    private void SaveVolume(string mixerChannel, float volume)
    {
        PlayerPrefs.SetFloat(mixerChannel, volume);
    }
    
    public void SetDefaultVolume()
    {
        SetMasterVolume(0.8f);
        SetMusicVolume(0.8f);
        SetFXVolume(0.8f);
        SetValueSlider(false);
    }

    private void SetValueSlider(bool valueDef = true)
    {
        SliderUI[] sliderUIs = FindObjectsOfType<SliderUI>();
        if (valueDef)
        {
            foreach (SliderUI slider in sliderUIs)
            {
                if (slider.GetVolumeName() == "volume") slider.SetVolume(masterVolume);
                else if (slider.GetVolumeName() == "music") slider.SetVolume(musicVolume);
                else if (slider.GetVolumeName() == "FX") slider.SetVolume(FXVolume);
            }
        }
        else
        {
            foreach (SliderUI slider in sliderUIs)
            {
                slider.SetVolume(0.8f);
            }
        }
    }
    #endregion

}
