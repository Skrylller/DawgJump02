using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlatformScript : MonoBehaviour
{

    public GameObject[] Particle = new GameObject[6];
    private GameObject[] PullParticle;
    private int counter;

    public GameObject[] ParticleDeath = new GameObject[1];
    private AudioSource Audio;
    public int Chance = 20;
    private int random;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        for (int i = 0; i < ParticleDeath.Length; i++)
        {
            ParticleDeath[i] = Instantiate(ParticleDeath[i], transform.position, Quaternion.identity);
            ParticleDeath[i].SetActive(false);
        }

        PullParticle = new GameObject[Particle.Length * 3];
        for (int i = 0; i < PullParticle.Length; i++)
        {
            int k = Random.Range(0, Particle.Length - 1);
            PullParticle[i] = Instantiate(Particle[k], transform.position, Quaternion.identity);
            PullParticle[i].transform.parent = transform;
            PullParticle[i].SetActive(false);
        }
        counter = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8 && collision.rigidbody.velocity.y <= 0)
        {
            random = Random.Range(0, 50);
            if(random < Chance)
            {
                Audio.volume = random / 20f;
                Audio.pitch = Random.Range(0.8f, 1.2f);
                Audio.PlayOneShot(Audio.clip);
                CreateParticle();
            }
        }

    }

    private void CreateParticle()
    {
        if(random > 0)
        {
            PullParticle[counter].SetActive(true);
            PullParticle[counter].transform.position = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y);
            counter++;
            if (counter >= PullParticle.Length)
            {
                counter = 0;
            }
            random--;
            StartCoroutine(Pause());
        }
    }
    public void CreateParticleDestroy()
    {
        for(int i = 0; i<ParticleDeath.Length; i++)
        {
            ParticleDeath[i].SetActive(true);
            ParticleDeath[i].transform.position = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y);
        }
    }

    IEnumerator Pause()
    {
        float ran2 = Random.Range(0, 0.3f);
        yield return new WaitForSeconds(Random.Range(0f, ran2));
        CreateParticle();
    }
}
