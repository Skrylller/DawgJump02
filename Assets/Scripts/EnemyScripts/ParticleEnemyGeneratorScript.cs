using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnemyGeneratorScript : MonoBehaviour
{
    public GameObject[] Particles = new GameObject[1];


    public void SpawnParticles()
    {
        for(int i = 0; i < Particles.Length; i++)
        {
            Instantiate(Particles[i], transform.position, Quaternion.identity);
        }
    }

}
