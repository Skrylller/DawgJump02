using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private PlayerMoveScript Player;
    public int direction;
    private bool isDown;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.isDown = false;
        Player.Null();
    }

    void Update()
    {
        if (!this.isDown) return;


        Player.Move(direction);

    }
}
