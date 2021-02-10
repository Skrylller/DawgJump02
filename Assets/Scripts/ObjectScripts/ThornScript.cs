using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour
{
    private Transform pl_Transform;
    private Rigidbody2D pl_Rigidbody2D;

    private void Awake()
    {
        pl_Rigidbody2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        pl_Transform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (pl_Rigidbody2D.velocity.y > 0 || pl_Transform.position.y - 0.7f < transform.position.y)
        {
            gameObject.layer = 10;
        }
        else
        {
            gameObject.layer = 11;
        }
    }

}