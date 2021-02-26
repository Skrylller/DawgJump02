using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerMoveScript Player;
    private Camera Camera;
    private RectTransform Size;
    private RectTransform trans;
    public bool isFront;
    public GameObject Center;
    public Transform Parent;
    private AttackButton attackButton;
    private bool isDown;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Size = GetComponent<RectTransform>();
        trans = GetComponent<RectTransform>();
        Size.sizeDelta = new Vector2(Size.sizeDelta.x, Camera.orthographicSize * 240);
        if (!isFront)
        {
            attackButton = GetComponentInChildren<AttackButton>();
        }

        trans.anchoredPosition = new Vector2(Size.position.x, Camera.orthographicSize * 200);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        this.isDown = true;
        if (Time.timeScale > 0)
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

    public void OnPointerUp(PointerEventData eventData)
    {
        this.isDown = false;
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

    void Update()
    {
        Size.sizeDelta = new Vector2(Size.sizeDelta.x, Camera.orthographicSize * 240);
        trans.anchoredPosition = new Vector2(Size.position.x, Camera.orthographicSize * 200);
        if (!this.isDown) return;


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
}
