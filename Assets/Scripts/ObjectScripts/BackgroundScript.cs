using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public Sprite[] BackgroundsOnLevel = new Sprite[1];
    private int counter;
    private const float backgroundsChunk = 45.5f;
    private float StartPosition;
    private Transform cam_Transform;
    private SpriteRenderer ObjSprite;
    public int Speed = 2;


    private void Awake()
    {
        cam_Transform = GameObject.Find("Main Camera").GetComponent<Transform>();
        ObjSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        counter = 1;
        StartPosition = transform.position.y;
        ObjSprite.sprite = BackgroundsOnLevel[0];
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, cam_Transform.position.y/Speed + StartPosition, transform.position.z);

        if (cam_Transform.position.y - backgroundsChunk >= transform.position.y)
        {
            PositionUpdate();
        }
    }

    private void PositionUpdate()
    {
        ObjSprite.sprite = BackgroundsOnLevel[counter];
        StartPosition = StartPosition + backgroundsChunk * 2;

        counter++;
        if (counter >= BackgroundsOnLevel.Length)
        {
            counter = 0;
        }
    }
}
