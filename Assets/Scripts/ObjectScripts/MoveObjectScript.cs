using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectScript : MonoBehaviour
{
    public Transform Destination;
    public float Speed; 



    void Awake()
    {
        Destination = GameObject.Find("ScrapGamePoint").GetComponent<Transform>();
    }

    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Destination.transform.position, Time.deltaTime * Speed);
        gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, gameObject.transform.localScale / 2, Time.deltaTime * 5f);
        if(Vector3.Distance(gameObject.transform.position, Destination.transform.position) <= 0.2f)
        {
            Destroy(gameObject);
        }
    }


}
