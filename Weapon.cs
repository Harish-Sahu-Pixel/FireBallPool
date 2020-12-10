using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Private Variables
    private float speed = 30.0f;
    private int powerupScore = 10;
    private int enemyScore = 5;
    private int coin = 1;
    private int weaponWasted = -10;
    private float weaponRange = 5.0f;
    private Rigidbody weaponRb;
    private PlayerController player;
    private SpawnManager spawnManager;
    private Score score;
    
    // Start is called before the first frame update
    void Start()
    {
        weaponRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        score = GameObject.Find("SpawnManager").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
        WeaponTranslate();
        DestroyWeapon();
    }

    //Method to translate the weapon
    private void WeaponTranslate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    //Method to Destroy the weapon
    private void DestroyWeapon()
    {
        if (transform.position.x > weaponRange || transform.position.z > weaponRange || transform.position.x < -weaponRange || transform.position.z < -weaponRange)
        {
            score.UpdateScore(weaponWasted);
            Destroy(gameObject);
        }
    }
    
    //On enter of the collision of two objects
    private void OnCollisionEnter(Collision collision)
    {
        //Collision with Powerup
        OnCollisionWithPowerUp(collision);

        //Collision with Enemy
        OnCollisionWithEnemy(collision);

        //Collision with Coin
        OnCollisionWithCoin(collision);
    }

    //OnCollision with Powerup
    private void OnCollisionWithPowerUp(Collision collision)
    {
        if (collision.gameObject.CompareTag("Powerup"))
        {
            if (player.isGameActive)
            {  
                spawnManager.TimerSetUp();
                score.UpdateScore(powerupScore);
                spawnManager.powerupParticle.transform.position = transform.position;
                spawnManager.powerupParticle.Play();
                player.hasPowerup = true;
                spawnManager.PowerUpTimer();
                spawnManager.Coroutine();
                player.ballHit.Play();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
    //OnCollision with Enemy
    private void OnCollisionWithEnemy(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (player.isGameActive)
            {
                score.UpdateScore(enemyScore);
                spawnManager.enemyParticle.transform.position = transform.position;
                spawnManager.enemyParticle.Play();
                player.ballHit.Play();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
    //OnCollision with Coin
    private void OnCollisionWithCoin(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            if (player.isGameActive)
            {
                score.UpdateCoins(coin);
                spawnManager.coinParticle.transform.position = transform.position;
                spawnManager.coinParticle.Play();
                spawnManager.SpawnRandomCoin();
                player.ballHit.Play();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}