using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D rb_Player;
    private ModeScript mode;
    private BoxCollider2D pl_Collider2D;
    private Vector3 AttackPoint;
    private CameraScript CameraScript;
    private Transform SmokeTransforrm;
    private AudioMaster Audio;
    public SpriteRenderer AttackIndicator;
    public GameObject IndicatorShild;
    public GameObject AttackWave;
    public GameObject TargetWave;
    public GameObject Smoke;
    public GameObject RebornUI;
    public GameObject PauseButton;
    public GameObject AttackSlayer;

    public float JumpForce;
    public float SetJumpForce = 7;
    public float Speed = 4;
    public float AttackDistance = 2;
    public float JumpPause = 0.3f;

    private bool bAttack;
    private bool AttackPause;
    private bool ChageJumpShild;
    private bool wasReborn;
    private bool hack;

    private byte shieldnum = 0;


    private void Awake()
    {
        Audio = GetComponentInChildren<AudioMaster>();
        SmokeTransforrm = GameObject.Find("MainGeneration").GetComponent<Transform>();
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        mode = GetComponent<ModeScript>();
        rb_Player = GetComponent<Rigidbody2D>();
        pl_Collider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        JumpForce = SetJumpForce;
        bAttack = false;
        AttackPause = true;
        shieldnum = 0;
        wasReborn = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            ChangeJump(15f);
            hack = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 100);
            hack = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            StartCoroutine(Shild(60f));
            hack = true;
        }

        if (mode.mode == 0)
        {
            pl_Collider2D.size = new Vector2(0.4f, 1.1f);
            pl_Collider2D.offset = new Vector2(0f, -0.45f);
        }
        else
        {
            pl_Collider2D.size = new Vector2(0.4f, 1.5f);
            pl_Collider2D.offset = new Vector2(0f, -0.2f);
        }

        if (bAttack)
        {
            transform.position = Vector3.Lerp(transform.position, AttackPoint, Time.deltaTime * 8);
            rb_Player.velocity = new Vector2(0, +0.01f);
        }

        if(rb_Player.velocity.y <= 0 && ChageJumpShild)
        {
            shieldnum--;
            StartCoroutine(Shild(0.3f));
            ChageJumpShild = false;
        }

        if(transform.position.y + 6 < CameraScript.transform.position.y && transform.position.y > -10 && rb_Player.velocity.y < 0)
        {
            if (gameObject.layer == 12 || wasReborn || PlayerPrefs.GetInt("QuantitySecondHealth", 0) == 0)
            {
                CameraScript.Death(hack);
            }
            else
            {
                RebornUI.SetActive(true);
                PauseButton.SetActive(false);
                Time.timeScale = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.layer == 11 || collision.gameObject.layer == 13) && !IndicatorShild.activeSelf) 
        {
            TakeDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 11 || collision.gameObject.layer == 13) && !IndicatorShild.activeSelf)
        {
            TakeDamage();
        }
    }

    public void Null()
    {
        rb_Player.velocity = new Vector2(0, rb_Player.velocity.y);
    }

    public void Move(int direction)
    {
        if (mode.mode != 0 && gameObject.layer == 8)
        {
            rb_Player.velocity = new Vector2(Speed * direction, rb_Player.velocity.y);
        }
        if (transform.position.x <= -3.35)
        {
            transform.position = new Vector2(3.34f, transform.position.y);
        }
        else if (transform.position.x >= 3.35)
        {
            transform.position = new Vector2(-3.34f, transform.position.y);
        }
    }

    public void Ground()
    {
        StartCoroutine(Jump());
        Instantiate(Smoke, transform.position, Quaternion.identity, SmokeTransforrm);

        Audio.AudioPlay(3); //fall on stone sound
        Audio.AudioPlay(0); //smoke on stone sound
    }

    public void Target(Vector3 a)
    {
        if (Vector2.Distance(a, transform.position) < 0.1f)
        {
            a.y = transform.position.y + 1;
            a.x = transform.position.x - 0.025f;
        }

        TargetWave.transform.LookAt(a, Vector3.forward);
    }
    public void Target(bool onTarget)
    {
        if (!onTarget || AttackPause)
        {
            TargetWave.SetActive(onTarget);
        }
    }

    public void Attack(Vector3 a)
    {
        float r;
        if (AttackPause && gameObject.layer == 8 && Time.timeScale != 0)
        {
            if (Vector2.Distance(a, transform.position) < 0.1f || a.x == 0 && a.y == 0)
            {
                r = 1;
                a.y = transform.position.y + 1;
                a.x = transform.position.x - 0.025f;
            }
            else
            {
                r = Mathf.Sqrt(Mathf.Pow(transform.position.x - a.x, 2) + Mathf.Pow(transform.position.y - a.y, 2));
            }

            AttackPoint = new Vector3(((a.x - transform.position.x) / r * AttackDistance) + transform.position.x, ((a.y - transform.position.y) / r * AttackDistance) + transform.position.y, transform.position.z);
            AttackWave.SetActive(true);
            if (PlayerPrefs.GetInt("AttackManagmentUI", 0) == 1)
            {
                AttackSlayer.SetActive(false);
            }
            StartCoroutine(Shild(1f));
            mode.SetMode(4);
            Audio.AudioPlay(1); //attack sound
            AttackWave.transform.position = gameObject.transform.position;
            AttackWave.transform.LookAt(a, Vector3.forward);

            StartCoroutine(attack());
        }
    }

    public void TakeDamage()
    {
        Audio.AudioPlay(4); //Damage sound
        CameraScript.Shaking(0.1f);
        TargetWave.SetActive(false);
        if (!wasReborn && PlayerPrefs.GetInt("QuantitySecondHealth", 0) != 0)
        {
            RebornUI.SetActive(true);
            PauseButton.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        Time.timeScale = CameraScript.timeSpeed;
        gameObject.layer = 12;
        rb_Player.velocity = new Vector2(0, -1f);
        mode.SetMode(3);
    }

    public void Reborn()
    {
        Audio.AudioPlay(0);
        Time.timeScale = CameraScript.timeSpeed;
        rb_Player.velocity = new Vector2(rb_Player.velocity.x, 20);
        mode.SetMode(2);
        StartCoroutine(Shild(8f));
        wasReborn = true;
    }

    public void FastStart()
    {
        Audio.AudioPlay(5); //smoke on stone sound
        rb_Player.velocity = new Vector2(rb_Player.velocity.x, 40f);
        if (!ChageJumpShild)
        {
            IndicatorShild.SetActive(true);
            shieldnum++;
            ChageJumpShild = true;
        }
    }

    public void StartGame()
    {
        AttackPause = false;
        AttackIndicator.color = new Vector4(255, 255, 255, 0);
        JumpForce = 15;
        mode.mode = 10;
        StartCoroutine(StartAttackPause());
    }

    public void ChangeJump(float newForce)
    {
        JumpForce = newForce;
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(JumpPause);
        if(mode.mode == 0)
        {
            Audio.AudioPlay(2); //jump sound
            rb_Player.velocity = new Vector2(rb_Player.velocity.x, JumpForce);
            if(JumpForce != SetJumpForce)
            {
                IndicatorShild.SetActive(true);
                shieldnum++;
                ChageJumpShild = true;
            }
            JumpForce = SetJumpForce;
        }
    }

    private IEnumerator attack()
    {
        AttackPause = false;
        AttackIndicator.color = new Vector4(1,1,1,0);
        bAttack = true;
        yield return new WaitForSeconds(0.4f);
        AttackWave.SetActive(false);
        mode.SetMode(1);
        bAttack = false;
        yield return new WaitForSeconds(2f);
        if (PlayerPrefs.GetInt("AttackManagmentUI", 0) == 1)
        {
            AttackSlayer.SetActive(true);
        }
        AttackPause = true;
        AttackIndicator.color = new Vector4(1, 1, 1, 0.7f);
    }

    public IEnumerator Shild(float time)
    {
        shieldnum++;
        IndicatorShild.SetActive(true);
        yield return new WaitForSeconds(time);
        shieldnum--;
        if(shieldnum <= 0)
        {
            IndicatorShild.SetActive(false);
        }
    }

    IEnumerator StartAttackPause()
    {
        yield return new WaitForSeconds(1f);
        AttackPause = true;
        AttackIndicator.color = new Vector4(1, 1, 1, 0.7f);
    }

}
