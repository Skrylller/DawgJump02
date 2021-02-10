using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDownPlayerScript : MonoBehaviour
{
    private Rigidbody2D pl;

    public GameObject Wave;
    public GameObject RestartButton;

    private void Awake()
    {
        pl = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        pl.velocity = new Vector2(0, -5);
    }

    private void Update()
    {
        if(gameObject.transform.position.y <= -3.2f)
        {
            RestartButton.SetActive(true);
            Wave.SetActive(true);
            Destroy(gameObject);
        }
    }
}
