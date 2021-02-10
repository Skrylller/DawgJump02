using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public SettingsScript Settings;
    public string NameSetting;
    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        float a = new float();
        a = PlayerPrefs.GetFloat(NameSetting, 0);
        if (a == 0)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

    public void Music()
    {
        if (toggle.isOn == true)
        {
            Settings.ChangeVolume(0, NameSetting);
        }
        else
        {
            Settings.ChangeVolume(-80, NameSetting);
        }
    }

}
