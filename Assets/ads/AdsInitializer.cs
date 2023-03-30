using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{

    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iOSGameId;
    [SerializeField] private bool _testMode = true;
    [SerializeField] private bool _isRewardable = true;


    private AdsSkippable _adsSkippable;
    //private AdsBanner _adsBanner;
    private RewardedAds _rewardedAds;
    private string _gameId;

    private void Awake()
    {
        _adsSkippable = GetComponent<AdsSkippable>();
       // _adsBanner = GetComponent<AdsBanner>();
        if (_isRewardable)
            _rewardedAds = GetComponent<RewardedAds>();

        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    private void OnEnable()
    {
        if (_isRewardable)
            _rewardedAds.LoadAd();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        //_adsBanner.LoadBanner();
        //_adsBanner.ShowBanner();
        _adsSkippable.LoadAd();

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
