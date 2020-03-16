using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsMenuButton : IMenuButton, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Menu")]
    [SerializeField] GameObject volumeMenu;
    [SerializeField] GameObject controlsMenu;
    private void Start()
    {
        Components();
    }

    private void Update()
    {
        AnimatorControls(menuButtonController, thisIndex, animator, isOver);
        OpenMenu();
    }

    private void OpenMenu()
    {
        if (menuButtonController.GetIndex() == 0)
        {
            volumeMenu.SetActive(true);
            controlsMenu.SetActive(false);
        }
        else if (menuButtonController.GetIndex() == 1)
        {
            volumeMenu.SetActive(false);
            controlsMenu.SetActive(true);
        }
        else
        {
            //volumeMenu.SetActive(false);
            //controlsMenu.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (menuButtonController.GetIndex() != thisIndex)
        {
            menuButtonController.SetIndex(thisIndex);
            menuButtonController.PlaySound(changeMenuSFX);
        }
        isOver = true;
    }
    public void OnPointerExit(PointerEventData eventData) => isOver = false;
    
    override public void OnPressedMenu()
    {
        if (menuButtonController.IsAlive())
        {
            if (thisIndex == menuButtonController.GetMaxIndex())
            {
                menuButtonController.PlaySound(selectMenuSFX);
                StartCoroutine(WaitAndEnable());
            }
        }
    }

    IEnumerator WaitAndEnable()
    {
        yield return new WaitForSeconds(waitTime);
        OnEnablePanel(firstPanel, secondPanel);
        volumeMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
}
