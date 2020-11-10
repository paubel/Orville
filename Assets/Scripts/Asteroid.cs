using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 5.0f;
    private float zDestroy = 0;
    private Rigidbody asteroidRb;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        asteroidRb = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        asteroidRb.AddForce(Vector3.forward * -speed);
        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
            if (spawnManager.isGameActive)
            {
                spawnManager.UpdateScore(-20);
            }
        }
    }
}
