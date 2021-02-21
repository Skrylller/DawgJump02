using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeastPointScript : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        int point;
        point = PlayerPrefs.GetInt("BeastPoint2", 0);
        if(point != 0)
        {
            text.text = ("Лучший результат:\n" + point.ToString());
        }
        else
        {
            text.text = ("");
        }
    }


    public void SetPointText(int point)
    {
        text.text = point.ToString();
    }
}
