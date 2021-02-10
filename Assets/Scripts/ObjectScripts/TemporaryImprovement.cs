using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryImprovement : MonoBehaviour
{
    private PlayerMoveScript Player;
    private AudioMaster Audio;

    public enum Improvement : int
    {
        Shild = 1,
        LongJump
    }

    public Improvement TypeImprovement;

    public float durationImprovement;

    private void Awake()
    {
        Audio =  GameObject.Find("SoundsObject").GetComponent<AudioMaster>();
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            ActiveImprovement();
        }
    }

    public void ActiveImprovement()
    {
        switch(TypeImprovement)
        {
            case Improvement.Shild:
                Player.StartCoroutine(Player.Shild(durationImprovement));
                break;

            case Improvement.LongJump:
                Player.ChangeJump(durationImprovement);
                break;
        }
        Audio.AudioPlay(0);
        Destroy(gameObject);
    }
}
