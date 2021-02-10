using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkript : MonoBehaviour
{
    private CameraScript CameraScript;
    private Transform Player;
    private Animator tur_Animator;
    private AudioMaster Audio;
    public Transform Parrent;
    public GameObject Shell;
    public GameObject Explosive;
    public GameObject Smoke;

    private float timer;
    private bool destroy;
    public float playerDistance = 5;
    public float timerSet = 2;
    

    private void Awake()
    {
        Audio = GetComponent<AudioMaster>();
        tur_Animator = GetComponent<Animator>();
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14 && !destroy)
        {
            Destroy();
        }
    }


    void Update()
    {
        if(Player.position.y <= transform.position.y && Player.position.y + playerDistance >= transform.position.y && !destroy)
        {
            Vector3 pos = Player.position - transform.position;
            float rotateZ = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f,0f,rotateZ + 90);

            if (timer <= 0)
            {
                Shoot();
            }
        }

        timer -= Time.deltaTime;
    }

    private void Shoot()
    {
        Audio.AudioPlay(0);
        CameraScript.Shaking(0.05f, 0.05f);
        Instantiate(Shell, transform.position, transform.rotation);
        timer = timerSet;
        Instantiate(Smoke, Parrent);
    }

    public void GenerateTerret()
    {
        destroy = false;
        tur_Animator.SetInteger("mode", 0);
    }

    public void Destroy()
    {
        Audio.AudioPlay(1);
        destroy = true;
        Instantiate(Explosive, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        CameraScript.Shaking(0.05f);
        tur_Animator.SetInteger("mode", 1);
    }
}
