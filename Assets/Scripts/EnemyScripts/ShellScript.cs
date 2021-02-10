using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    private CameraScript CameraScript;
    public GameObject Explosive;
    public float Speed = 1;

    private void Awake()
    {
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }

    private void Start()
    {
        Destroy(gameObject, 10);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14 || collision.gameObject.layer == 8)
        {
            Destroy();
        }
    }

    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
        if(transform.localPosition.y < 6.5f)
        {
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        Instantiate(Explosive, transform.position, Quaternion.identity);
        CameraScript.Shaking(0.1f);
        Destroy(gameObject);
    }
}
