using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    private PlayerMoveScript Player;
    public int direction;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
    }

    public void OnMouseDrag()
    {
        Player.Move(direction);
    }

    public void OnMouseUp()
    {
        Player.Null();
    }
}
