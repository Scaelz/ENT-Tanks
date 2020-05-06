using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioSource changeMenu;
    [SerializeField] AudioSource selectMenu;

    AudioClip changeMenuSFX;
    AudioClip selectMenuSFX;

    public AudioSource GetChangeMenuSFX() => changeMenu;
    public AudioSource GetSelectMenuSFX() => selectMenu;

}
