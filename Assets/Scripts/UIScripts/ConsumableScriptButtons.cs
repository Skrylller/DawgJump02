using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableScriptButtons : MonoBehaviour
{
    public Text text;
    private PlayerMoveScript Player;
    private int quantity;
    private AudioMaster Audio;
    public string Name;

    private void Start()
    {
        Audio = GameObject.Find("SoundsObject").GetComponent<AudioMaster>();
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
        quantity = PlayerPrefs.GetInt(Name, 0);
        text.text = ("= " + quantity);
    }


    public void ConsumadleUsing()
    {
        if(CheckQuantity())
        {
            PlayerPrefs.SetInt(Name, quantity);
            Audio.AudioPlay(3);
            switch (Name)
            {
                case "QuantitySecondHealth":
                    Player.Reborn();
                    break;
                case "QuantityFastStart":
                    Player.FastStart();
                    gameObject.SetActive(false);
                    break;
            }
        }
    }

    private bool CheckQuantity()
    {
        if (quantity > 0)
        {
            quantity--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
