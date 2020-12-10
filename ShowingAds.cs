using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowingAds : MonoBehaviour
{
    //Private Variables
    private string googlePlayID = "3558738";
    private string myPlacementID = "video";
    private bool testMode = false;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(googlePlayID, testMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method for showing interstitial Ads
    public void ShowInterstitialAds()
    { 
        Advertisement.Show(myPlacementID);
    }
}
