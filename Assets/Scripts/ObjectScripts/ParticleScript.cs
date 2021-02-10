using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceMove;
    public float forceRotation;
    private Vector3 Rotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(Random.Range(forceMove, -forceMove), Random.Range(forceMove, -forceMove));
        forceRotation = Random.Range(forceRotation, -forceRotation);
        Rotation = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        Rotation = new Vector3(0, 0, Rotation.z + forceRotation * Time.deltaTime);
        transform.eulerAngles = Rotation;
    }

}
