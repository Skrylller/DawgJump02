using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapScript : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        int Scrap = PlayerPrefs.GetInt("Scrap", 0);
        text.text = Scrap.ToString();
    }
}
