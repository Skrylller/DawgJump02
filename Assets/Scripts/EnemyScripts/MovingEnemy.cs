using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [HideInInspector]
    public Transform MainCamera;
    [HideInInspector]
    public Transform Player;
    [HideInInspector]
    public ParticleEnemyGeneratorScript Particle;
    [HideInInspector]
    public AudioMaster Audio;
    [HideInInspector]
    public GenerationScript Generator;
    [HideInInspector]
    public CameraScript cameraScript;

    [HideInInspector]
    public Vector2 AimPosition;
    [HideInInspector]
    public bool ReadyAttack;
    [HideInInspector]
    public bool Dammage;
    [HideInInspector]
    public bool Attack;
    [HideInInspector]
    public float SpeedMultiplier;
    public float Speed;
    public int Health;


    public void Awake()
    {
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        MainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
        Particle = GetComponent<ParticleEnemyGeneratorScript>();
        Audio = GameObject.Find("SoundsObject").GetComponent<AudioMaster>();
        Generator = GameObject.Find("MainGeneration").GetComponent<GenerationScript>();
    }

    public void Start()
    {
        SpeedMultiplier = 1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14 && !Dammage)
        {
            TakeDammage();
        }
    }

    public void TakeDammage()
    {
        Dammage = true;
        gameObject.layer = 0;
        SpeedMultiplier = 0;
        Audio.AudioPlay(2);
        StartCoroutine(DammagePause());
        Health--;
        if (Health <= 0)
        {
            Generator.CheckEnemy();
            Audio.AudioPlay(1);
            Particle.SpawnParticles();
            gameObject.SetActive(false);
        }
    }
    public IEnumerator DammagePause()
    {
        AimPosition = transform.position;
        yield return new WaitForSeconds(1f);
        SpeedMultiplier = 1;
        Dammage = false;
        gameObject.layer = 13;
    }

    public IEnumerator AttackPause()
    {
        yield return new WaitForSeconds(2f);
        ReadyAttack = true;
    }

}
