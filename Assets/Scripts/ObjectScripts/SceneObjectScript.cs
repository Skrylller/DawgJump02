using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectScript : MonoBehaviour
{
    private Transform cam_Transform;

    public float DistanceFromCamera = 5.5f;
    public bool isDestroyer;


    private void Awake()
    {
        cam_Transform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Update()
    {
        if (cam_Transform.position.y - DistanceFromCamera > transform.position.y)
        {
            if (isDestroyer)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
