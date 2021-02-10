using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private Animator animator;
    private CameraScript CameraScript;
    private AudioSource Audio;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 14 || collision.gameObject.layer == 8)
        {
            Audio.pitch = Random.Range(0.9f, 1.1f);
            Audio.Play();
            CameraScript.Shaking(0.2f);
            animator.SetInteger("Mode", 1);
            StartCoroutine(Pause());
        }
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = 0;
    }
}
