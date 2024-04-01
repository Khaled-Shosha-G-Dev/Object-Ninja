using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
                  //the script that we will take a data about a our game 
    private GameManager gameManager;
                  //physics to objeact
    private Rigidbody targetRb;
                  //object speed
    private float minSpeed = 12;
    private float maxSpeed = 16;
                  //torque delta to object
    private float torqueVal = 10;
                  //the objects bounded
    private float xRange = 4;
    private float yPos = -2;

    [SerializeField] private int pointvalue; //amount of score when a player destory this object

    [SerializeField] private ParticleSystem explotionEffect; //particle system start when an object destroy

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        
        transform.position =RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    private float RandomTorque()
    {
        return Random.Range(-torqueVal, torqueVal);
    }
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(xRange, -xRange), yPos);
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explotionEffect, transform.position, explotionEffect.transform.rotation);
            gameManager.UpdateScore(pointvalue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateChances();
            if(gameManager.chanceNum<1) 
                gameManager.GameOver();
        }
    }
}
