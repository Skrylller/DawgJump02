using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapChanceScript : MonoBehaviour
{

    public int ScrapChance;
    public int ScrapAmmount;
    public SaveProgressScript Scrap;
    public GameObject ScrapEffect;

    private void Awake()
    {
        Scrap = GameObject.Find("Main Camera").GetComponent<SaveProgressScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            int random = Random.Range(1,100);

            if (random <= ScrapChance)
            {
                Instantiate(ScrapEffect, gameObject.transform.position, Quaternion.identity);
                Scrap.ChangeScrap(ScrapAmmount);
            }
        }
    }
}
