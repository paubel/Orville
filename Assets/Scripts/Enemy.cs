using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 20.0f;
    private float zDestroy = 0;
    private Rigidbody enemyRb;
    private GameObject player;
    private SpawnManager spawnManager;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);

        }
        else
        {
            enemyRb.AddForce(-Vector3.forward * speed);
        }
        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
            if (spawnManager.isGameActive)
            {
                spawnManager.UpdateScore(-10);
            }
        }
    }
}
