using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapMoneyScript : MonoBehaviour
{


    public SaveProgressScript Scrap;
    public GameObject ScrapEffect;
    private AudioMaster Audio;

    public void Awake()
    {
        Audio =  GameObject.Find("SoundsObject").GetComponent<AudioMaster>();
        Scrap = GameObject.Find("Main Camera").GetComponent<SaveProgressScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Audio.AudioPlay(0);
            Scrap.ChangeScrap(1);
            Instantiate(ScrapEffect, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
