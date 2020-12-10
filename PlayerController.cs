using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Private Variables
    private float rotationSpeed = 90.0f;
    private float speed ;
    private float powerupStrength = 25.0f;
    private float turnInput;
    private int hasPowerupScore = 10;
    private int temp = 0;
    private bool turnLeft = false;
    private bool turnRight = false;
    private Score score;
    private UIDecisions uiDecide;
    private SavingData savingData;
    private SavingCoinData savingCoinData;

    //Public Variables
    public bool hasPowerup = false;
    public bool isGameActive = false;
    public GameObject weaponPrefab;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI totalCoinsText;
    public AudioClip ballHitSound;
    public AudioClip ballBlastSound;
    public AudioClip gameOverSound;
    public AudioSource ballHit;

    // Start is called before the first frame update
    void Start()
    {
        ballHit = GetComponent<AudioSource>();
        uiDecide = GameObject.Find("UIGameObject").GetComponent<UIDecisions>();
        savingData = GameObject.Find("DataManager").GetComponent<SavingData>();
        savingCoinData = GameObject.Find("DataManager").GetComponent<SavingCoinData>();
        score = GameObject.Find("SpawnManager").GetComponent<Score>();
    }

    //Method for PointerDownLeft
    public void PointerDownLeft()
    {
        turnLeft = true;
    }

    //Method for PointerUpLeft
    public void PointerUpLeft()
    {
        turnLeft = false;
    }

    //Method for PointerDownRight
    public void PointerDownRight()
    {
        turnRight = true;
    }

    //Method for PointerUpRight
    public void PointerUpRight()
    {
        turnRight = false;
    }

    //Method for deciding the direction to turn for the player
    private void TurningPlayer()
    {
        if(turnLeft)
        {
            turnInput = -rotationSpeed;
        }
        else if (turnRight)
        {
            turnInput = rotationSpeed;
        }
        else
        {
            turnInput = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for Keyboard Inputs
        //HorizontalInput();
        TurningPlayer();

        //For the Usage of gameOver Code
        PlayerDisappear();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * turnInput * Time.deltaTime);
    }

    //Method for left right input for Keyboard
    private void HorizontalInput()
    {
        //Taking the input from the user via left and right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        //Setting up the rotation speed for the field
        transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * rotationSpeed);
    }

    //Method for MobileWeaponInput
    public void MobileWeaponInput()
    {
        Vector3 offset = new Vector3(0.0f, 0.25f, 0.0f);
        Instantiate(weaponPrefab, transform.position + offset, transform.rotation);
    }

    //weapon Input through Keyboard
    private void WeaponInput()
    {
        Vector3 offset = new Vector3(0.0f, 0.25f, 0.0f);

        //Weapon Instantiate
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(weaponPrefab, transform.position + offset, transform.rotation);
        }
    }

    //On collision enter of the player
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            ballHit.PlayOneShot(ballHitSound, 1.0f);
            score.UpdateScore(hasPowerupScore);
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
        if(collision.gameObject.CompareTag("Enemy") && !hasPowerup)
        {
            isGameActive = false;
            uiDecide.field.transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
            uiDecide.launch.SetActive(false);
            uiDecide.leftButton.SetActive(false);
            uiDecide.rightButton.SetActive(false);
            uiDecide.fireParticleGroup.transform.position = new Vector3(360.0f, 0.0f, 0.0f);
            uiDecide.mainCamera.transform.position = new Vector3(360.0f, 12.0f, -15.0f);
            uiDecide.gameOverText.SetActive(true);

            //Saving Coin and HighScore Data
            SavingTotalCoinData();
            SavingHighScoreData();
            
            highScoreText.gameObject.SetActive(true);
            totalCoinsText.gameObject.SetActive(true);
            uiDecide.continueText.SetActive(true);
            uiDecide.watchAdButton.SetActive(true);
            uiDecide.spendCoinsButton.SetActive(true);

            //Activating the menu Button
            uiDecide.menuButton.SetActive(true);
            ballHit.PlayOneShot(gameOverSound, 1.0f);
        }
    }

    //Method to remove the player upon gameOver and repositioning it when GameActive is true
    private void PlayerDisappear()
    {
        if(!isGameActive)
        {
            transform.position = new Vector3(0.0f, 40.0f, 0.0f);
        }
        else
        {
            transform.position = new Vector3(0.0f, 1.25f, -4.5f);
        }
    }

    //Method for Saving Coin Data
    private void SavingTotalCoinData()
    {
        //saving coin data
        savingCoinData.LoadData();
        score.tempCoins = score.coins;
        score.tempCoins = score.tempCoins - temp;
        score.tempCoins = score.tempCoins + savingCoinData.coinTotal.totalCoinCount;
        savingCoinData.SaveData();
        savingCoinData.LoadData();
        totalCoinsText.text = "Total Coins : " + savingCoinData.coinTotal.totalCoinCount;
        score.tempCoins = score.coins;
        temp = score.coins;
    }

    //Method for Saving HighScore Data
    private void SavingHighScoreData()
    {
        //saving highscore data
        savingData.LoadData();
        if (score.score > savingData.best.highScore)
        {
            savingData.SaveData();
            savingData.LoadData();
        }
        highScoreText.text = "HighScore : " + savingData.best.highScore;
    }
}