using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private PlayerMoveScript Player;
    private Camera Camera;
    private BoxCollider2D Col;
    private RectTransform trans;
    public bool isFront;
    public GameObject Center;
    public Transform Parent;
    private AttackButton attackButton;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Col = GetComponent<BoxCollider2D>();
        trans = GetComponent<RectTransform>();
        Col.size = new Vector2(Col.size.x, Camera.orthographicSize * 240);
        if (!isFront)
        {
            attackButton = GetComponentInChildren<AttackButton>();
        }

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
            if (isFront)
            {
                Player.Target(true);
            }
            else
            {
                Parent.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                Center.SetActive(true);
                attackButton.AttackOnMouseDown();
            }
        }
    }

    private void OnMouseDrag()
    {
        if (Time.timeScale > 0)
        {
            if (isFront)
            {
                Vector3 Aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Aim = new Vector3(Aim.x, Aim.y, 0);
                Player.Target(Aim);
            }
            else
            {
                attackButton.AttackOnMouseDrag();
            }
        }
    }

    private void OnMouseUp()
    {
        if (Time.timeScale > 0)
        {
            if (isFront)
            {
                Player.Target(false);
                Vector3 Aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Aim = new Vector3(Aim.x, Aim.y, 0);
                Player.Attack(Aim);
            }
            else
            {
                attackButton.AttackOnMouseUp();
                Center.SetActive(false);
            }
        }
    }
}
