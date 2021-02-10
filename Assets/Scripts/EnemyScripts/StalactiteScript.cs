using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteScript : MonoBehaviour
{
    public GameObject Attention;

    private Camera Camera;
    private GameObject attention;
    private Vector3 position;
    public GameObject[] Particle = new GameObject[6];
    private GameObject[] PullParticle;
    private int counter;
    private AudioSource Audio;
    private int random;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        attention = Instantiate(Attention, position, Quaternion.identity);
        attention.SetActive(false);
        PullParticle = new GameObject[Particle.Length * 3];
        for (int i = 0; i < PullParticle.Length; i++)
        {
            int k = Random.Range(0, Particle.Length - 1);
            PullParticle[i] = Instantiate(Particle[k],transform.position, Quaternion.identity);
            PullParticle[i].transform.parent = transform;
            PullParticle[i].SetActive(false);
        }
        counter = 0;
    }

    private void Update()
    {
        attention.transform.position = new Vector3(position.x, Camera.transform.position.y + (Camera.orthographicSize - 0.4f), position.z);
        if(attention.transform.position.y >= transform.position.y && position.z != -10)
        {
            position.z = -10;
        }

        if (Camera.transform.position.y == 0)
        {
            Destroy();
        }
    }

    public void GenerateStalactite()
    {
        position = new Vector3(transform.position.x, Camera.transform.position.y + (Camera.orthographicSize - 0.4f), 0);
        attention.SetActive(true);
        random = Random.Range(30, 50);
        Audio.volume = random / 50f;
        Audio.pitch = Random.Range(0.8f, 1.2f);
        CreateParticle();
    }

    public void Destroy()
    {
        attention.SetActive(false);
        gameObject.SetActive(false);
    }

    private void CreateParticle()
    {
        if (random > 0)
        {
            PullParticle[counter].SetActive(true);
            PullParticle[counter].transform.position = new Vector2(transform.position.x + Random.Range(-0.3f, 0.7f), transform.position.y - Random.Range(10, 30));
            counter++;
            if(counter >= PullParticle.Length)
            {
                counter = 0;
            }
            random--;
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        float ran2 = Random.Range(0, 0f);
        yield return new WaitForSeconds(Random.Range(0f, ran2));
        CreateParticle();
    }
}
