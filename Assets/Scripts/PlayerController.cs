using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float horizontalInput;
    private float verticalInput;
    public float xRange = 5.5f;
    public float yRange = 3.0f;
    public GameObject projectilePrefab;
    public GameObject explosPlayer;
    private SpawnManager spawnManager;
    private float fireRate = 1.0f;
    bool allowfire = true;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        //Keep the player in bounds
        ConstrainPlayerPosition();


        if (Input.GetKeyDown(KeyCode.Space) && allowfire)
        {
            // No longer necessary to Instantiate prefabs
            // Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            StartCoroutine(FireDelay());
            // Get an object object from the pool
            GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.position = transform.position; // position it at player
            }
        }

    }
    IEnumerator FireDelay()
    {
        allowfire = false;
        yield return new WaitForSeconds(fireRate);
        allowfire = true;
    }
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(-Vector3.forward * Time.deltaTime * speed * verticalInput);
    }
    private void ConstrainPlayerPosition()
    {
        //x-direction
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //y-direction
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }

        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Kaylon"))
        {
            Debug.Log("Player collided with Enemy");
            Destroy(gameObject);
            spawnManager.GameOver();
            Instantiate(explosPlayer, transform.position, explosPlayer.transform.rotation);

        }
        /*
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Player collided with Asteroid");
            Destroy(gameObject);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Life"))
        {
            Debug.Log("Player trigged Life");
            Destroy(other.gameObject);
        }
    }
}
