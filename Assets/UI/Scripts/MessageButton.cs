using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageButton : IMenuButton, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject messagePanel;
    private void Start()
    {
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
            if (thisIndex == 0)
            {
                StartCoroutine(WaitAndQuitGame());
            }
            else if (thisIndex == 1)
            {
                StartCoroutine(WaitAndEnableMessageBox());
            }
        }
    }
    IEnumerator WaitAndEnableMessageBox()
    {
        yield return new WaitForSeconds(waitTime);
        OnEnablePanelBox(messagePanel, firstPanel);
    }
    void OnEnablePanelBox(GameObject objectHide, GameObject objectShow)
    {
        objectHide.SetActive(false);
        objectHide.GetComponent<MenuButtonController>().SetIsAlive(false);
        objectShow.GetComponent<MenuButtonController>().SetIsAlive(true);
        objectShow.SetActive(true);
    }
}
