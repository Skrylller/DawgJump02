using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public SettingsScript Settings;
    public string NameSetting;
    public float defValue;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(NameSetting, defValue);
    }

    public void SliderMusic()
    {
        try
        {
            Settings.ChangeVolume((int)slider.value, NameSetting);
        }
        catch{}
    }

    public void Transparency()
    {
        try
        {
            PlayerPrefs.SetFloat(NameSetting, slider.value);
        }
        catch { }
    }
}
