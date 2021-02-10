using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreningTriggerScript : MonoBehaviour
{
    public GameObject menu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }
    }

}
