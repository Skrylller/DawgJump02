using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public AudioMixerGroup Mixer;


    private void Start()
    {
        float a;
        a = PlayerPrefs.GetFloat("MasterVolume", 0);
        if(a == 0)
        {
            Mixer.audioMixer.SetFloat("MasterVolume", 0);
        }
        else
        {
            Mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
        Mixer.audioMixer.SetFloat("BackgroundVolume", PlayerPrefs.GetFloat("BackgroundVolume", 0)); 
        Mixer.audioMixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("SoundVolume", 0));
    }

    public void ChangeVolume(int value, string Setting)
    {
        Mixer.audioMixer.SetFloat(Setting, value);
        PlayerPrefs.SetFloat(Setting, value);
    }
}
