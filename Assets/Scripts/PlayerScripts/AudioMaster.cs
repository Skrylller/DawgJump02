using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{

    [System.Serializable]
    public class Sound
    {
        public AudioClip audioClip;
        public float Volume = 1;
        public float pitch = 1;
    }

    private AudioSource Audio;
    public Sound[] Sounds = new Sound[1];

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void AudioPlay(int num)
    {
        Audio.volume = Sounds[num].Volume;
        Audio.pitch = Random.Range(Sounds[num].pitch - 0.1f, Sounds[num].pitch + 0.1f);
        Audio.PlayOneShot(Sounds[num].audioClip);
    }
    public void AudioPlay(int num, float pitch)
    {
        Audio.volume = Sounds[num].Volume;
        Audio.pitch = Random.Range(Sounds[num].pitch - pitch, Sounds[num].pitch + pitch);
        Audio.PlayOneShot(Sounds[num].audioClip);
    }
}
