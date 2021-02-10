using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpPlatformScript : MonoBehaviour
{
    private Transform pl_Transform;
    private Rigidbody2D pl_Rigidbody2D;
    private ParticlePlatformScript Particle;
    private BoxCollider2D pl_Collider;
    private Transform Camera;
    public Transform Sprite;
    private float DistanceDestroy;

    private void Awake()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        Particle = GetComponent<ParticlePlatformScript>();
        pl_Collider = GetComponent<BoxCollider2D>();
        pl_Rigidbody2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        pl_Transform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        DistanceDestroy = 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && collision.rigidbody.velocity.y <= 0)
        {
            PlatformDown();
        }

    }

    private void Update()
    {
        if (pl_Transform.transform.position.y - 0.8f >= transform.position.y && pl_Rigidbody2D.velocity.y <= 0)
        {
            gameObject.layer = 9;
            pl_Collider.isTrigger = false;
        }
        else if(pl_Transform.transform.position.y + 0.5f < transform.position.y && pl_Rigidbody2D.velocity.y > 0)
        {
            gameObject.layer = 11;
            pl_Collider.isTrigger = true;
        }
        else if (pl_Transform.transform.position.y - 0.8f < transform.position.y && pl_Rigidbody2D.velocity.y > 0)
        {
            gameObject.layer = 10;
            pl_Collider.isTrigger = false;
        }

        if (transform.position.y + DistanceDestroy < Camera.position.y)
        {
            Particle.CreateParticleDestroy();
            gameObject.SetActive(false);
        }
    }

    private void PlatformDown()
    {
        Vector3 pos = Sprite.position;
        Sprite.position = Vector3.MoveTowards(Sprite.position, new Vector3(Sprite.position.x, Sprite.position.y - 0.05f, Sprite.position.z), Time.deltaTime);
        StartCoroutine(PauseBack(pos));
    }
    IEnumerator PauseBack(Vector3 pos)
    {
        yield return new WaitForSeconds(0.1f);
        Sprite.position = pos;
    }

}