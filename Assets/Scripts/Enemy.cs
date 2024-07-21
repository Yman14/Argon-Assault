using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathFX;
    [SerializeField] GameObject hitVFX;
    [Tooltip("Damaged Per Hit")] [SerializeField] int dph = 15;
    [SerializeField] int health = 50;

    Scoreboard scoreboard;
    GameObject parentObject;

    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        parentObject = GameObject.FindWithTag("SpawnAtRuntime");

        //Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        //rb.GetComponent<Rigidbody>().useGravity = false;
    }


    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if(health <= 0)
        {
            KillEnemy();
        }
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         KillEnemy();
    //     }
    // }
    

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        scoreboard.UpdateScore(dph);
        health -= dph;

        vfx.transform.parent = parentObject.transform;
    }

    void KillEnemy()
    {
        GameObject fx = Instantiate(enemyDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentObject.transform;

        Destroy(gameObject);
    }
}
