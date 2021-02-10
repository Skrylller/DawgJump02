using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public RectTransform ParentTransform;
    public GameObject distanceSprite;
    private PlayerMoveScript Player;
    public float distance;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerMoveScript>();
    }

    private void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            distanceSprite.SetActive(true);
            Player.Target(true);
        }
    }

    private void OnMouseDrag()
    {
        if(Time.timeScale != 0)
        {
            if (Mathf.Pow(distance, 2) >= Mathf.Pow(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - ParentTransform.position.x, 2) + Mathf.Pow(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - ParentTransform.position.y, 2))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Time.deltaTime * 10);
            }
            else
            {
                float r = Mathf.Sqrt(Mathf.Pow(ParentTransform.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 2) + Mathf.Pow(ParentTransform.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 2));
                transform.position = Vector3.Lerp(transform.position,new Vector3((((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - ParentTransform.transform.position.x) / r * distance) + ParentTransform.transform.position.x), (((Camera.main.ScreenToWorldPoint(Input.mousePosition).y - ParentTransform.transform.position.y) / r * distance) + ParentTransform.transform.position.y), ParentTransform.transform.position.z), Time.deltaTime * 10);
            }
            Vector3 AimAttack = new Vector3(Player.transform.position.x - ParentTransform.position.x + transform.position.x, Player.transform.position.y - ParentTransform.position.y + transform.position.y, 0);
            Player.Target(AimAttack);

        }
    }
    private void OnMouseUp()
    {

        if (Time.timeScale != 0)
        {
            distanceSprite.SetActive(false);
            Player.Target(false);
            Vector3 AimAttack = new Vector3(Player.transform.position.x - ParentTransform.position.x + transform.position.x, Player.transform.position.y - ParentTransform.position.y + transform.position.y, 0);
            Player.Attack(AimAttack);
            transform.position = ParentTransform.position;
        }
    }
}
