using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CameraScript : MonoBehaviour
{
    public enum Lokation : byte
    {
        Smith,
        Hub
    }

    private Vector3 position;
    private Transform pl_Transform;
    public PlayerMoveScript Player;
    private SaveProgressScript SaveProgressScript;
    private AudioSource Audio;
    public AudioMixerGroup Mixer;

    public GameObject Pause;
    public GameObject GameUI;
    public GameObject MenuUI;
    public GameObject SmithyUI;
    public GameObject FastStart;

    public GameObject plDeath;

    private Lokation lokation;

    public Transform tr_Smithy;
    public Transform tr_Hub;

    private bool gamestart;

    private float power;
    private bool shakeCheck;

    public const float timeSpeed = 1f;

    //traning
    public GameObject[] TraningUI = new GameObject[1];

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
        pl_Transform = GameObject.Find("Player").GetComponent<Transform>();
        SaveProgressScript = GetComponent<SaveProgressScript>();
        Time.timeScale = timeSpeed;
    }

    private void Start()
    {
        lokation = Lokation.Hub;
        gamestart = false;
        position = transform.position;
        try
        {
            SmithyUI.SetActive(false);
        }
        catch { }
    }

    private void Update()
    {
        if (position.y < pl_Transform.position.y - 1 && gamestart)
        {
            position = new Vector3(tr_Hub.position.x, pl_Transform.position.y - 1, position.z);
        }

            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 5);



        if(shakeCheck == true && Time.timeScale != 0)
        {
            float Ran = Random.Range(-power, power);
            gameObject.transform.position = new Vector3(Ran, transform.position.y, transform.position.z);
        }
    }

    public void Death(bool hack)
    {
        Player.transform.position = new Vector2(0, -10);
        position = new Vector3(0, 0, -10);
        transform.position = new Vector3(0, 0, -10);
        gamestart = false;
        GameUI.SetActive(false);
        plDeath.SetActive(true);
        if (!hack)
        {
            SaveProgressScript.SaveProgress();
        }
    }

    public void Shaking(float time)
    {
        power = 0.1f;
        StartCoroutine(ShakePause(time));
    }
    public void Shaking(float time, float pow)
    {
        power = pow;
        StartCoroutine(ShakePause(time));
    }

    public void RestartGame()
    {
        Audio.Play();
        SceneManager.LoadScene(0);
    }

    public void ActivePause()
    {
        Audio.Play();
        Pause.SetActive(true);
        Time.timeScale = 0;
    }
    public void UnactivePause()
    {
        Audio.Play();
        Pause.SetActive(false);
        Time.timeScale = timeSpeed;
    }

    public void StartGame()
    {
        Audio.Play();
        lokation = Lokation.Hub;
        gamestart = true;
        GameUI.SetActive(true);
        MenuUI.SetActive(false);
        Player.StartGame();
        if (PlayerPrefs.GetInt("QuantityFastStart", 0) == 0)
        {
            FastStart.SetActive(false);
        }
        else
        {
            StartCoroutine(DeactivateFastStart());
        }
    }

    public void TrainingOne()
    {
        Audio.Play();
        lokation = Lokation.Hub;
        gamestart = true;
        TraningUI[0].SetActive(false);
        TraningUI[7].SetActive(true);
        TraningUI[6].SetActive(true);
        Time.timeScale = timeSpeed;
        Player.StartGame();
    }

    public void TrainingTwo()
    {
        TraningUI[1].SetActive(false);
        TraningUI[2].SetActive(true);
        TraningUI[6].SetActive(true);
    }

    public void TrainingThree()
    {
        TraningUI[2].SetActive(false);
        TraningUI[5].SetActive(true);
        TraningUI[6].SetActive(true);
        Time.timeScale = timeSpeed;
    }

    public void TrainingFour()
    {
        TraningUI[3].SetActive(false);
        TraningUI[5].SetActive(true);
        TraningUI[6].SetActive(true);
        Time.timeScale = timeSpeed;
    }

    public void TrainingFive()
    {
        PlayerPrefs.SetInt("Trening", 1);
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int value)
    {
        SceneManager.LoadScene(value);
    }

    public void MovePosition(int move)
    {
        Audio.Play();
        lokation += (byte)move;

        switch (lokation)
        {
            case Lokation.Smith:
                MenuUI.SetActive(false);
                position = new Vector3(tr_Smithy.position.x, tr_Smithy.position.y, transform.position.z);
                transform.position = position;
                SmithyUI.SetActive(true);
                Mixer.audioMixer.SetFloat("BackgroundVolume", (PlayerPrefs.GetFloat("BackgroundVolume", 0)+80)/1.2f-80);
                break;
            case Lokation.Hub:
                SmithyUI.SetActive(false);
                position = new Vector3(tr_Hub.position.x, tr_Hub.position.y, transform.position.z);
                transform.position = position;
                Mixer.audioMixer.SetFloat("BackgroundVolume", PlayerPrefs.GetFloat("BackgroundVolume", 0));
                MenuUI.SetActive(true);
                break;
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void LeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }

    IEnumerator ShakePause(float time)
    {
        shakeCheck = true;
        yield return new WaitForSeconds(time);
        shakeCheck = false;
        gameObject.transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    IEnumerator DeactivateFastStart()
    {
        yield return new WaitForSeconds(10f);
        FastStart.SetActive(false);
    }
}
