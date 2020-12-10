using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    //Public Variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public int score;
    public int coins;
    public int tempCoins;

    // Start is called before the first frame update
    void Start()
    {
        StartScore();
    }

    //My Start
    public void StartScore()
    {
        score = 0;
        coins = 0;
        UpdateScore(0);
        UpdateCoins(0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Method to UpdateScore
    public void UpdateScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        scoreText.text = "Score : " + score;
    }

    //Method to Update Coins
    public void UpdateCoins(int coinsToAdd)
    {
        coins = coins + coinsToAdd;
        coinsText.text = "Coins : " + coins;
    }
}