using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private PlayerMoveScript Player;
    private Camera Camera;
    private BoxCollider Col;
    private RectTransform trans;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Col = GetComponent<BoxCollider>();
        trans = GetComponent<RectTransform>();
        Col.size = new Vector2(Col.size.x, Camera.orthographicSize * 240);

        trans.anchoredPosition = new Vector2(Col.transform.position.x, Camera.orthographicSize * 200);
    }

    public void Update()
    {

        Col.size = new Vector2(Col.size.x, Camera.orthographicSize * 240);
        trans.anchoredPosition = new Vector2(Col.transform.position.x, Camera.orthographicSize * 200);
    }

    private void OnMouseDown()
    {
        if(Time.timeScale > 0)
        {
            Player.Target(true);
        }
    }

    private void OnMouseDrag()
    {
        if (Time.timeScale > 0)
        {
            Vector3 Aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Aim = new Vector3(Aim.x, Aim.y, 0);
            Player.Target(Aim);
        }
    }

    private void OnMouseUp()
    {
        if (Time.timeScale > 0)
        {
            Player.Target(false);
            Vector3 Aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Aim = new Vector3(Aim.x, Aim.y, 0);
            Player.Attack(Aim);
        }
    }
}
