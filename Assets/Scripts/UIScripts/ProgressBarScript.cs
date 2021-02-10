using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour
{
    [System.Serializable]
    private class ProgressPoint
    {
        public int Point;
        public int Scrap;
    }

    private Slider slider;
    private SaveProgressScript ProgressScript;
    private GenerationScript Generator;
    private int LevelPoint;
    private AudioMaster Audio;
    public GameObject ScrapEffect;
    public Text textBefore, textAfter;

    [SerializeField]
    private ProgressPoint[] Points = new ProgressPoint[1];

    private void Awake()
    {
        slider = GetComponent<Slider>();
        ProgressScript = GameObject.Find("Main Camera").GetComponent<SaveProgressScript>();
        Audio = GameObject.Find("SoundsObject").GetComponent<AudioMaster>();
        Generator = GameObject.Find("MainGeneration").GetComponent<GenerationScript>();
    }

    private void Start()
    {
        LevelPoint = 0;
        slider.maxValue = Points[LevelPoint].Point;
        textBefore.text = "0";
        textAfter.text = Points[LevelPoint].Point.ToString();
    }

    private void Update()
    {
        int value = ProgressScript.ReturPoint();
        slider.value = value;
        if(value >= slider.maxValue)
        {
            UpPoint();
        }
    }

    private void UpPoint()
    {
        ProgressScript.ChangeScrap(Points[LevelPoint].Scrap);
        if(LevelPoint < Points.Length - 1)
        {
            slider.minValue = Points[LevelPoint].Point;
            LevelPoint++;
            Generator.PointLevelUP();
            slider.maxValue = Points[LevelPoint].Point;
        }
        else
        {
            slider.minValue = slider.maxValue;
            slider.maxValue += 50000;
        }
        textBefore.text = slider.minValue.ToString();
        textAfter.text = slider.maxValue.ToString();
        Instantiate(ScrapEffect, gameObject.transform.position, Quaternion.identity);
        Audio.AudioPlay(0);
    }
}
