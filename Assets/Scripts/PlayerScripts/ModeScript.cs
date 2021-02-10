using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeScript : MonoBehaviour
{
    public int mode;
    private Rigidbody2D rb_Player;
    private PlayerMoveScript Player;
    private Animator pl_Animator;

    /*
     * -1-хаб
     * 
     * 0-покой
     * 1-падение
     * 2-полет
     * 3-смеерть
     * 4-атака
     */

    private void Awake()
    {
        pl_Animator = GameObject.Find("PlayerSprite").GetComponent<Animator>();
        Player = GetComponent<PlayerMoveScript>();
        rb_Player = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mode = -1;
    }

    private void Update()
    {
        if(rb_Player.velocity.y < 0 && mode != 4)
        {
            SetMode(1);
        }
        else if (rb_Player.velocity.y > 0 && mode != 4)
        {
            SetMode(2);
        }
        else if (rb_Player.velocity.y == 0 && mode > 0)
        {
            Player.Null();
            Player.Ground();
            SetMode(0);
        }
    }

    public void SetMode(int m)
    {
        if(mode != 3 && mode >= 0)
        {
            mode = m;
            pl_Animator.SetInteger("mode", mode);
        }
    }
}
