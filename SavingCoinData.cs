using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SavingCoinData : MonoBehaviour
{
    //Private Variables
    private string DATA_PATH = "/MyCoinData.dat";
    private Score score;

    //Public Variable
    public CoinCount coinTotal;
    
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("SpawnManager").GetComponent<Score>();
        print(Application.persistentDataPath + DATA_PATH);
    }


    // Update is called once per frame
    void Update()
    {

    }

    //Method to Save Data
    public void SaveData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + DATA_PATH);
            coinTotal = new CoinCount();
            coinTotal.totalCoinCount = score.tempCoins;
            bf.Serialize(file, coinTotal);
        }
        catch (Exception e)
        {
            if (e != null)
            {
                //handle Exception
            }
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //Method to LoadData
    public void LoadData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);
            coinTotal = bf.Deserialize(file) as CoinCount;
        }
        catch (Exception e)
        {
            if (e != null)
            {
                //handle Exception
            }
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}