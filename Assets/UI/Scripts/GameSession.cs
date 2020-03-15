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

    private void Start()
    {
        menuButtonController = optionMenu.GetComponent<MenuButtonController>();
        isOptions = menuButtonController.IsAlive();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && optionMenu != null)
        {
            isOptions = menuButtonController.IsAlive();
            if (isOptions)
            {
                isOptions = false;
                menuButtonController.SetIndex(0);
            }
            else
            {
                isOptions = true;
            }
            optionMenu.SetActive(isOptions);
            menuButtonController.SetIsAlive(isOptions);
        }
        if (isOptions && Input.GetKeyDown(KeyCode.Return) && menuButtonController.GetIndex() == 2 || Input.GetKeyDown(KeyCode.Mouse0) && menuButtonController.GetIndex() == 2)
        {
            StartCoroutine(WaitAndSet());
        }
    }
    IEnumerator WaitAndSet()
    {
        yield return new WaitForSeconds(0.4f);
        menuButtonController.SetIndex(0);
    }
    public void SetVolume(float volume) => CheckVolume("volume", volume);
    public void SetMusicVolume(float volume) => CheckVolume("music", volume);
    public void SetFXVolume(float volume) => CheckVolume("FX", volume);

    private void CheckVolume(string mixerChannel, float volume)
    {
        audioMixer.SetFloat(mixerChannel, Mathf.Lerp(-80, 0, Mathf.Pow(volume, 0.25f)));
    }
}
