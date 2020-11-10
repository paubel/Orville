using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 40;
    private SpawnManager spawnManager;
    //public ParticleSystem explosionParticle;
    public GameObject explosAsteroid;
    public GameObject explosKaylon;


    private Rigidbody laserboltRb;
    void Start()
    {
        laserboltRb = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            //Debug.Log("Laserbolt collided with Asteroid 5p");
            //Destroy(gameObject);
            gameObject.SetActive(false);
            spawnManager.UpdateScore(5);
            Destroy(collision.collider.gameObject);
            Instantiate(explosAsteroid, transform.position, explosAsteroid.transform.rotation);


        }
        if (collision.gameObject.CompareTag("Kaylon"))
        {
            //Debug.Log("Laserbolt collided with Kaylon 10p");

            //Destroy(gameObject);
            gameObject.SetActive(false);
            spawnManager.UpdateScore(10);
            Destroy(collision.collider.gameObject);
            Instantiate(explosKaylon, transform.position, explosKaylon.transform.rotation);



        }
        /*
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Player collided with Asteroid");
            Destroy(gameObject);
        }*/
    }
}
