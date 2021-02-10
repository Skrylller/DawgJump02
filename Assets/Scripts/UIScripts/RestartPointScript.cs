using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartPointScript : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetNewBestPointText(int point)
    {
        text.text = ("Новый рекорд!\n" + point.ToString());
    }

    public void NewPointText(int point)
    {
        text.text = ("Ваш результат:\n" + point.ToString());
    }
}
