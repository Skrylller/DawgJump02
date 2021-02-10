using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScript : MonoBehaviour
{
    private Text text;


    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.text = ("0");
    }

    public void SetPointText(int point)
    {
        text.text = point.ToString();
    }
}
