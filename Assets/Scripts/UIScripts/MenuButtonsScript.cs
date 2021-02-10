using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsScript : MonoBehaviour
{
    public GameObject UIActivated, UIDeactivated;
    public AudioSource Audio;

    public void UIActive()
    {
        UIActivated.SetActive(true);
        Audio.Play();
    }
    public void UIChange()
    {
        UIDeactivated.SetActive(false);
        UIActivated.SetActive(true);
        Audio.Play();
    }

    public void UIDeactive()
    {
        UIDeactivated.SetActive(false);
        Audio.Play();
    }

}
