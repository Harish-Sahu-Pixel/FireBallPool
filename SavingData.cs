using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SavingData : MonoBehaviour
{
    //Private Variables
    private string DATA_PATH = "/MyGame.dat";
    private Score score;

    //Public Variable
    public HighScore best;
   

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
            best = new HighScore();
            best.highScore = score.score;
            bf.Serialize(file, best);
        }
        catch(Exception e)
        {
            if(e != null)
            {
                //handle Exception
            }
        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }

    //Method to Load Data
    public void LoadData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);
            best = bf.Deserialize(file) as HighScore;
        }
        catch(Exception e)
        {
            if (e != null)
            {
                //handle Exception
            }
        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }
}