using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    //Private variables
    private string googlePlayId = "3558738";
    private UIDecisions uIDecisions;
    private bool isWatched = false;

    Button myButton;
    
    //Public variables
    public string myPlacementId = "rewardedVideo";
    public TextMeshProUGUI warningText;
    ShowResult result;
    

    void Start()
    {
        uIDecisions = GameObject.Find("UIGameObject").GetComponent<UIDecisions>();
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(googlePlayId, false);
    }

    private void Update()
    {
       
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        if(!isWatched)
        {
            Advertisement.Show(myPlacementId);
            result = ShowResult.Finished;
            OnUnityAdsDidFinish(myPlacementId, result);
            isWatched = true;
        }
        else
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "You Already Watched an AD";
            StartCoroutine(TextNull());
        }
    }

    IEnumerator TextNull()
    {
        yield return new WaitForSeconds(1);
        warningText.text = "";
        warningText.gameObject.SetActive(false);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            uIDecisions.WatchAnAd();
            
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}