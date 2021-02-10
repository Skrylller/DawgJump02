using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebornScript : MonoBehaviour
{
    public Text text;
    private PlayerMoveScript Player;
    private int quantity;
    private AudioMaster Audio;

    private void Start()
    {
        Audio = GameObject.Find("SoundsObject").GetComponent<AudioMaster>();
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
        quantity = PlayerPrefs.GetInt("QuantitySecondHealth", 0);
        text.text = ("= " + quantity);
    }


    public void Reborn()
    {
        if(quantity > 0)
        {
            quantity--;
            PlayerPrefs.SetInt("QuantitySecondHealth", quantity);
            Audio.AudioPlay(3);
            Player.Reborn();
        }
    }
}
