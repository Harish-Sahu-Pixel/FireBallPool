using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    //Private Variables
    private float spawnRangeX = 3.0f;
    private float spawnRangeZ = 3.0f;
    private float startDelay = 2.0f;
    private float powerUpDelay = 16.0f;
    private float powerUpTime = 8.0f;
    private PlayerController player;
    private float timer = 1.0f;
    private int setTimer = 8;

    //Public Variables
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public GameObject coinPrefab;
    public TextMeshProUGUI timerText;
    public ParticleSystem powerupParticle;
    public ParticleSystem enemyParticle;
    public ParticleSystem coinParticle;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    //My Start
    public void StartSpawnManager(float spawnRate)
    {
        //Method call to repeat the function "SpawnFunction" at a specified spawnRate
        if(player.isGameActive)
        {
            InvokeRepeating("SpawnFunction", startDelay, spawnRate);
            SpawnRandomPowerup();
            SpawnRandomCoin();
        } 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Method to SpawnFunction
    private void SpawnFunction()
    {
        if(player.isGameActive)
        {
            SpawnRandomEnemy();
        }  
    }

    //Method to spawn coin
    public void SpawnRandomCoin()
    {
        if(player.isGameActive)
        {
            //Using PowerupSpawn method for Spawning Coins 
            Vector3 coinSpawn = PowerupSpawn();
            Instantiate(coinPrefab, coinSpawn, coinPrefab.transform.rotation);
        }
    }

    //Method to Spawn Random enemy
    private void SpawnRandomEnemy()
    {
        if(player.isGameActive)
        {
            Vector3 enemySpawn = EnemySpawn();
            Instantiate(enemyPrefab, enemySpawn, enemyPrefab.transform.rotation);
        } 
    }

    //Method to spawn Random Powerup
    public void SpawnRandomPowerup()
    {
        if(player.isGameActive)
        {
            Vector3 powerupSpawn = PowerupSpawn();
            Instantiate(powerUpPrefab, powerupSpawn, powerUpPrefab.transform.rotation);
        }
    }

    //Method to get spawnPosition for the enemy
    private Vector3 EnemySpawn()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1.75f, Random.Range(-1.0f, spawnRangeZ));
        return spawnPos;
    }

    //Method to get spawnPosition for the Powerup
    private Vector3 PowerupSpawn()
    {
        Vector3 spawnPosPowerUp = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1.75f, Random.Range(-2.0f, spawnRangeZ));
        return spawnPosPowerUp;
    }

    //IEnumerator fo Powerup delay
    IEnumerator PowerupDelay()
    {
        yield return new WaitForSeconds(powerUpDelay);
        SpawnRandomPowerup();
    }

    //Coroutine for Powerup delay
    public void Coroutine()
    {
        StartCoroutine(PowerupDelay());
    }

    //IEnumerator for determining the time of the powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpTime);
        player.hasPowerup = false;
    }

    //Coroutine for PowerupTimer
    public void PowerUpTimer()
    {
        StartCoroutine(PowerupCountdownRoutine());
    }

    IEnumerator PowerUpTimerSetUp()
    {
        yield return new WaitForSeconds(timer);
        setTimer--;
        timerText.text = "0" + setTimer;
        if (setTimer!=0)
        {
            StartCoroutine(PowerUpTimerSetUp());
        }
        else
        {
            timerText.text = "";
            setTimer = 8;
        }
    }

    //Method for Text timer Setup for the Power up indicator
    public void TimerSetUp()
    {
        timerText.text = "0" + setTimer;
        StartCoroutine(PowerUpTimerSetUp());
    }

}