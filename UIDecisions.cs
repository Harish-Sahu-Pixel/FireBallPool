using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIDecisions : MonoBehaviour
{
    //Private Variables
    private float easySpawnRate = 3.0f;
    private float mediumSpawnRate = 2.0f;
    private float hardSpawnRate = 1.0f;
    private int coinsSpent = 25;
    private SpawnManager spawnManager;
    private PlayerController playerController;
    private Score score;
    private ShowingAds adManager;
    private SavingCoinData coinData;
   
    //Public Variables
    public GameObject fireParticleGroup;
    public GameObject mainCamera;
    public GameObject launch;
    public GameObject uiMenu;
    public GameObject gameOverText;
    public GameObject menuButton;
    public GameObject watchAdButton;
    public GameObject spendCoinsButton;
    public GameObject continueText;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject field;
    public TextMeshProUGUI warningText;
    public Text instructionsText;
    public Text staySafeText;

    // Start is called before the first frame update
    void Start()
    {
        coinData = GameObject.Find("DataManager").GetComponent<SavingCoinData>();
        adManager = GameObject.Find("AdManager").GetComponent<ShowingAds>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        score = GameObject.Find("SpawnManager").GetComponent<Score>();
        playerController =GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method for EasyButton
    public void EasyButton()
    {
        SetActiveCode();
        spawnManager.StartSpawnManager(easySpawnRate);
    }

    //Method for MediumButton
    public void MediumButton()
    {
        SetActiveCode();
        spawnManager.StartSpawnManager(mediumSpawnRate);
    }

    //Method for HardButton
    public void HardButton()
    {
        SetActiveCode();
        spawnManager.StartSpawnManager(hardSpawnRate);
    }

    //Method for Tutorial Button
    public void HowToPlay()
    {
        //........
        adManager.ShowInterstitialAds();
        uiMenu.SetActive(false);
        instructionsText.gameObject.SetActive(true);
        staySafeText.gameObject.SetActive(true);
        menuButton.SetActive(true);
    }

    //Method for Menu Button
    public void MenuButton()
    {
        adManager.ShowInterstitialAds();
        SceneManager.LoadScene(0);
    }

    //Method for watch an Ad Button
    public void WatchAnAd()
    {
        playerController.highScoreText.gameObject.SetActive(false);
        playerController.totalCoinsText.gameObject.SetActive(false);
        gameOverText.SetActive(false);
        continueText.SetActive(false);
        menuButton.SetActive(false);
        watchAdButton.SetActive(false);
        spendCoinsButton.SetActive(false);
        launch.SetActive(true);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        mainCamera.transform.position = new Vector3(0.0f, 12.0f, -15.0f);
        fireParticleGroup.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        field.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        playerController.isGameActive = true;
    }
    

    //Method for spend coins button
    public void SpendCoinsButton()
    {
        coinData.LoadData();
        if (coinData.coinTotal.totalCoinCount >= coinsSpent)
        {
            score.tempCoins = coinData.coinTotal.totalCoinCount - coinsSpent;
            coinData.SaveData();
            coinData.LoadData();
            WatchAnAd();
        }
        else
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "Not enough coins!! Watch an ad to continue";
            StartCoroutine(TextNull());
        }
    }

    IEnumerator TextNull()
    {
        yield return new WaitForSeconds(1);
        warningText.text = "";
        warningText.gameObject.SetActive(false);
    }

    //SetActive code for all the easy,medium and hard buttons in the UI
    private void SetActiveCode()
    {
        playerController.isGameActive = true;
        uiMenu.SetActive(false);
        launch.SetActive(true);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        score.scoreText.gameObject.SetActive(true);
        score.coinsText.gameObject.SetActive(true);
        mainCamera.transform.position = new Vector3(0.0f, 12.0f, -15.0f);
        fireParticleGroup.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
}