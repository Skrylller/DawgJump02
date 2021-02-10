using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithySectionsScript : MonoBehaviour
{
    public int Section = 0;
    public GameObject[] Sections = new GameObject[1];
    public SpriteRenderer[] Sprites = new SpriteRenderer[1];
    private AudioSource Audio;



    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        Change();
        transform.position = new Vector3(0, 0, 0);
    }


    public void ChangeSections(int value)
    {
        Audio.Play();
        Section = value;
        Change();
    }
    public void Change()
    {
        for (int i = 0; i < Sections.Length; i++)
        {
            Sections[i].SetActive(false);
            Sprites[i].color = new Vector4(1, 1, 1, 1);
        }
        Sections[Section].SetActive(true);
        Sprites[Section].color = new Vector4(0.7f, 0.7f, 0.7f, 1);
    }
}
