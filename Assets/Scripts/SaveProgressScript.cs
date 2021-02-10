using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class SaveProgressScript : MonoBehaviour
{
    private Transform cam_Transform;
    private int point;
    private int Scrap;
    private int BestPoint;

    private const string Liderboard = "CgkI5uzlw9oTEAIQAA";

    public PointScript pointText;
    public RestartPointScript RestartPointText;


    private void Awake()
    {
        cam_Transform = GameObject.Find("Main Camera").GetComponent<Transform>();
        if (PlayerPrefs.GetInt("Trening") == 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void Start()
    {
        BestPoint = PlayerPrefs.GetInt("BeastPoint", 0);
        Scrap = PlayerPrefs.GetInt("Scrap", 0);

        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ReportScore(BestPoint, Liderboard, (bool success2) => { });
            }
            else
            {

            }
        });
    }


    private void Update()
    {
        if (cam_Transform.position.y - 5f > 0)
        {
            point = (int)((cam_Transform.position.y - 5f) * 100);
            pointText.SetPointText(point);
        }
        ScrapPlus();
    }

    public void SaveProgress()
    {
        if (point > BestPoint)
        {
            BestPoint = point;
            PlayerPrefs.SetInt("BeastPoint", BestPoint);
            Social.ReportScore(BestPoint, Liderboard, (bool success2) => { });
            RestartPointText.SetNewBestPointText(point);
        }
        else
        {
            RestartPointText.NewPointText(point);
        }
    }
    
    public bool ChangeScrap(int ValueChange)
    {
        if(Scrap >= -ValueChange)
        {
            Scrap += ValueChange;
            PlayerPrefs.SetInt("Scrap", Scrap);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ScrapPlus()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeScrap(10);
        }
    }

    public int ReturPoint()
    {
        return point;
    }
}
