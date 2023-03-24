using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements; //Assuming you imported the Advertisements from the "Package Manager"

public class AdsBanner : MonoBehaviour
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";

    private void Start()
    {
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER); //Positions Banner where you want -
    }

    public void ShowBanner()
    {
        Debug.Log("Unity Ads initialization complete.");


        Advertisement.Banner.Show(_androidAdUnitId);
    }

    public void LoadBanner()
    {
        Advertisement.Banner.Load(_androidAdUnitId);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}