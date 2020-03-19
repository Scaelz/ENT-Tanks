using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButton : IMenuButton, IPointerEnterHandler, IPointerExitHandler
{
    string nameGameScene;
    [SerializeField] GameObject messagePanel;
    private void Start()
    {
        nameGameScene = "Test Options";
        GetStartComponents();
    }

    private void Update()
    {
        AnimatorControls();
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
            menuButtonController.PlaySound(selectMenuSFX);
            if (thisIndex == 1)
            {
                StartCoroutine(WaitAndEnable());
            }
            OnLoadScene(thisIndex);
        }
    }

    public void OnLoadScene(int sceneIndex)
    {
        if (sceneIndex == 0)
        {
            StartCoroutine(WaitAndLoadGame());
        }
        else if (sceneIndex == menuButtonController.GetMaxIndex())
        {
            StartCoroutine(WaitAndEnableMessageBox());
        }
    }

    IEnumerator WaitAndLoadGame()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nameGameScene);
    }

    IEnumerator WaitAndEnableMessageBox()
    {
        yield return new WaitForSeconds(waitTime);
        OnEnablePanelBox(firstPanel, messagePanel);
    }
    void OnEnablePanelBox(GameObject objectHide, GameObject objectShow)
    {
        //objectHide.SetActive(false);
        objectHide.GetComponent<MenuButtonController>().SetIsAlive(false);
        objectShow.GetComponent<MenuButtonController>().SetIsAlive(true);
        objectShow.SetActive(true);
    }
}
