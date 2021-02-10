using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationScript : MonoBehaviour
{

    [System.Serializable]
    public class SceneObject
    {
        public GameObject GeneratedObject;
        public int ChanceGenerate;
        public int quantityObjects = 0;
        [HideInInspector]
        public int LocalChance = 0;
        [HideInInspector]
        public int counter = 0;
        [HideInInspector]
        public GameObject[] pullObjects;

        public void InstantiatePull(GameObject parent)
        {
            for(int i = 0; i < pullObjects.Length; i++)
            {
                pullObjects[i] = Instantiate(GeneratedObject, new Vector3(0,0,0), Quaternion.identity);
                pullObjects[i].transform.parent = parent.transform;
                pullObjects[i].SetActive(false);
            }
        }

        public GameObject Generate(Vector2 position)
        {
            try
            {
                pullObjects[counter].SetActive(true);
                pullObjects[counter].transform.position = position;
                int f = counter;
                counter++;
                if (counter > quantityObjects - 1)
                {
                    counter = 0;
                }
                return pullObjects[f];
            }
            catch
            {
                Debug.Log("Object adsent: " + counter);
                return null;
            }
        }
    }
    [System.Serializable]
    public class ChangingSceneObject : SceneObject
    {
        public int[] ChanceGenerateToLevel = new int[1];
    }

    public int PointLevel = 0;

    public int TwoPlatformChance;
    public int TwoThornChance;

    public ChangingSceneObject[] Platforms = new ChangingSceneObject[1];
    private int PlatformsMaxChance;


    public ChangingSceneObject Thorn = new ChangingSceneObject();

    public ChangingSceneObject[] Enemy = new ChangingSceneObject[1];
    private int EnemyMaxChance;

    public ChangingSceneObject[] MovingEnemy = new ChangingSceneObject[1];
    private int MovingEnemyMaxChance;
    private bool HaveMovingEnemy;

    public ChangingSceneObject Scrap = new ChangingSceneObject();

    public SceneObject[] TI = new SceneObject[1];
    private int TIMaxChance;

    private Transform cam_Transform;
    private float Chunk = 6;
    public float ChunkDistance = 2.2f;//2.2f

    private void Awake()
    {
        cam_Transform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    private void Start()
    {
        PointLevel = 0;
        HaveMovingEnemy = false;

        InstantiateAll();

        ChangeChance();
        CalculationChances(TI, out TIMaxChance);
        TIMaxChance += 200;
    }

    private void Update()
    {
        if (cam_Transform.position.y + 8 > Chunk)
        {
            Vector2 position = new Vector2(Random.Range(-2.2f, 2.2f), Chunk);


            CalculatePlatform(position);

            CalculateTI(position);

            if(Chunk > 15f)
            {
                CalculateEnemy();

                CalculateMovingEnemy();
            }

            CalculateScrap();

            Chunk += Random.Range(0.5f, ChunkDistance);
        }
    }

    private void InstantiateAll()
    {
        InstantiateObj(Platforms);
        InstantiateObj(Thorn);
        InstantiateObj(Enemy);
        InstantiateObj(MovingEnemy);
        InstantiateObj(Scrap);
        InstantiateObj(TI);
    }

    private void InstantiateObj(SceneObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].pullObjects = new GameObject[obj[i].quantityObjects];
            obj[i].InstantiatePull(gameObject);
        }
    }
    private void InstantiateObj(SceneObject obj)
    {
        obj.pullObjects = new GameObject[obj.quantityObjects];
        obj.InstantiatePull(gameObject);
    }

    private void CalculatePlatform(Vector2 position)
    {
        int randomTwoPlatform = Random.Range(1, 100);
        if (randomTwoPlatform <= TwoPlatformChance)
        {
            CreatePlatform(new Vector2(Random.Range(0f, 2.2f), Chunk));
            CreatePlatform(new Vector2(Random.Range(-2.2f, 0f), Chunk));
        }
        else
        {
            CreatePlatform(position);
        }
    }

    private void CreatePlatform(Vector2 position)
    {
        int random = Random.Range(1, PlatformsMaxChance);

        for (int i = 0; i < Platforms.Length; i++)
        {
            if(random <= Platforms[i].LocalChance)
            {
                if(i == 3)
                {
                    TurretSkript turret = Platforms[i].Generate(position).GetComponentInChildren<TurretSkript>();
                    turret.GenerateTerret();
                }
                else
                {
                    Platforms[i].Generate(position);
                    CalculateThorn(position);
                }
                i = Platforms.Length;
            }
        }
    }

    private void CalculateMovingEnemy()
    {
        if (!HaveMovingEnemy)
        {
            int random = Random.Range(1, MovingEnemyMaxChance);

            for (int i = 0; i < MovingEnemy.Length; i++)
            {
                if (random <= MovingEnemy[i].LocalChance)
                {
                    Vector2 position = new Vector2(0, Chunk + 10);
                    HaveMovingEnemy = true;

                    MovingEnemy[i].Generate(position);

                    i = MovingEnemy.Length;
                }
            }
        }   
    }

    private void CalculateEnemy()
    {
        int random = Random.Range(1, EnemyMaxChance);

        for (int i = 0; i < Enemy.Length; i++)
        {
            if (random <= Enemy[i].LocalChance)
            {
                Vector2 position;

                switch (i)
                {
                    case 1:
                        position = new Vector2(Random.Range(-2.2f, 2.2f), Chunk + 50);
                        StalactiteScript stalactite = Enemy[i].Generate(position).GetComponent<StalactiteScript>();
                        stalactite.GenerateStalactite();
                        break;
                    case 2:
                        position = new Vector2(Random.Range(-2.2f, 2.2f), Chunk + Random.Range(0f, 5f));
                        Enemy[i].Generate(position);
                        break;
                    default:
                        position = new Vector2(Random.Range(-2.2f, 2.2f), Chunk + Random.Range(0f, 2f));
                        Enemy[i].Generate(position);
                        break;
                }


                i = Platforms.Length;
            }
        }
    }

    private void CalculateTI(Vector2 position)
    {
        int random = Random.Range(1, TIMaxChance);

        for (int i = 0; i < TI.Length; i++)
        {
            if (random <= TI[i].LocalChance)
            {
                position = new Vector2(position.x, position.y + 1);
                TI[i].Generate(position);

                i = Platforms.Length;
            }
        }
    }

    private void CalculateThorn(Vector2 position)
    {
        int randomThorn = Random.Range(1, 100);
        
        if(randomThorn <= Thorn.ChanceGenerate)
        {
            randomThorn = Random.Range(1, 100);
            if (randomThorn <= TwoThornChance)
            {
                CreateThorn(position);
                CreateThorn(position);
            }
            else
            {
                CreateThorn(position);
            }
        }
    }

    private void CreateThorn(Vector2 position)
    {
        position = new Vector2(position.x + Random.Range(-0.4f, 0.4f), position.y + 0.3f);
        Thorn.Generate(position);
    }

    private void CalculationChances(SceneObject[] sceneObject, out int MaxChance)
    {
        MaxChance = 0;

        for (int i = 0; i < sceneObject.Length; i++)
        {
            MaxChance += sceneObject[i].ChanceGenerate;
            sceneObject[i].LocalChance = MaxChance;
        }
    }

    private void CalculateScrap()
    {
        int random = Random.Range(1, 100);
        if(random <= Scrap.ChanceGenerate)
        {
            Vector2 position = new Vector2(Random.Range(-2.2f, 2.2f), Chunk + Random.Range(0f, 5f));
            Scrap.Generate(position);
        }
    }

    public void ChangeChance()
    {
        ChangeChanceArray(Platforms, out PlatformsMaxChance);

        ChangeChanceArray(Enemy, out EnemyMaxChance);
        EnemyMaxChance += 90;

        ChangeChanceArray(MovingEnemy, out MovingEnemyMaxChance);

        Thorn.ChanceGenerate = Thorn.ChanceGenerateToLevel[PointLevel];

        Scrap.ChanceGenerate = Scrap.ChanceGenerateToLevel[PointLevel];

    }

    public void ChangeChanceArray(ChangingSceneObject[] obj, out int MaxChance)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].ChanceGenerate = obj[i].ChanceGenerateToLevel[PointLevel];
        }
        CalculationChances(obj,out MaxChance);
    }

    public void PointLevelUP() 
    {
        if(Thorn.ChanceGenerateToLevel.Length - 1 > PointLevel)
        {
            PointLevel++;
            ChangeChance();
        }
    }

    public void CheckEnemy()
    {
        Debug.Log("gg");
        StartCoroutine(CheckEnemyCourotine(1, 10));
    }

    IEnumerator CheckEnemyCourotine(float a,float b)
    {
        yield return new WaitForSeconds(Random.Range(a, b));
        HaveMovingEnemy = false;
    }

}
