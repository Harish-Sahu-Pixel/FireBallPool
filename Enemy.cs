using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Private Variables
    private float lowerBound = -20.0f;
    private float force = 2.0f;
    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFollower();
        DestroyEnemy();
    }

    //Method to Follow the Player
    private void PlayerFollower()
    {
        Vector3 Pos = new Vector3(0.0f, 1.25f, -4.5f);
        enemyRb.AddForce((Pos - transform.position).normalized * force);
    }

    //Method to Destroy the enemy
    void DestroyEnemy()
    {
        if(transform.position.y<lowerBound)
        {
            Destroy(gameObject);
        }
    }
}