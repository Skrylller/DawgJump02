using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithyScript : MonoBehaviour
{
    [System.Serializable]
    public class Levels
    {
        public float Power;
        public int Cost;
    }

    public enum Improvement : int
    {
        None = 1,
        Shild,
        LongJump,
        ForceJump,
        TimeJump
    }
    public enum Consumables : int
    {
        None = 1,
        SecondHealth
    }

    public Consumables TypeConsumables = Consumables.None;

    public Improvement TypeImprovement = Improvement.None;

    public TemporaryImprovement TI;
    public ConsumablesScript consumable;
    private SaveProgressScript Scrap;
    private Text text;
    private PlayerMoveScript Player;
    public AudioSource Audio;

    private int Level;

    public Levels[] levels = new Levels[1];



    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
        Scrap = GameObject.Find("Main Camera").GetComponent<SaveProgressScript>();
        text = GetComponentInChildren<Text>();
        if(TypeImprovement != Improvement.None)
        {
            Level = PlayerPrefs.GetInt("Level" + TypeImprovement, 0);
        }
        else
        {
            Level = 0;
        }
        Change();
    }

    private void Start()
    {

    }

    public void Change()
    {
        if(TypeImprovement != Improvement.None)
        {
            string word = ("");
            switch (TypeImprovement)
            {
                //TI
                case Improvement.Shild:
                    TI.durationImprovement = levels[Level].Power;
                    word = ("Длительность Щита ");
                    break;

                case Improvement.LongJump:
                    TI.durationImprovement = levels[Level].Power;
                    word = ("Сила Ус. Прыжка ");
                    break;
                //PI
                case Improvement.ForceJump:
                    Player.SetJumpForce = levels[Level].Power;
                    word = ("Сила прыжка ");
                    break;
                case Improvement.TimeJump:
                    Player.JumpPause = levels[Level].Power;
                    word = ("Задержка прыжка ");
                    break;
            }
            if ((Level + 1) < levels.Length)
            {
                text.text = ("Уровень: " + (Level + 1).ToString() + "\t\t" + word + levels[Level].Power.ToString() + "\nСтоимость улучшения: " + Mathf.Abs(levels[Level].Cost).ToString());
            }
            else
            {
                text.text = ("Уровень: " + (Level + 1).ToString() + "\t\t" + word + levels[Level].Power.ToString() + "\nМаксимальный уровень");
            }
        }
        else if(TypeConsumables != Consumables.None)
        {
            switch (TypeConsumables)
            {
                case Consumables.SecondHealth:
                    text.text = ("Количество дополнительных жизней: " + PlayerPrefs.GetInt("QuantitySecondHealth", 0).ToString() + "\nСтоимость покупки: " + Mathf.Abs(levels[0].Cost).ToString());
                    break;
            }
        }
    }

    public void LevelUP()
    {
        if ((Level+1) < levels.Length)
        {
            if (Scrap.ChangeScrap(levels[Level].Cost))
            {
                Audio.Play();
                if (TypeImprovement != Improvement.None)
                {
                    Level++;
                    PlayerPrefs.SetInt("Level" + TypeImprovement, Level);
                    Change();
                }
            }
        }
        else if (TypeConsumables != Consumables.None)
        {
            if (Scrap.ChangeScrap(levels[Level].Cost))
            {
                Audio.Play();
                consumable.PlusConsumables(1);
                Change();
            }
        }
    }
}
