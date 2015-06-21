using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public static class AdsManeger
{
    public enum AdsType { HeyzApp, AdMob };

    private static bool isInited = false;
    public static AdsType currentAdsType = AdsType.AdMob;

    //for addMOB
    private static InterstitialAd interstitial;



    public static void fetchInterstitial()
    {
       
        switch (currentAdsType)
        {
            case AdsType.HeyzApp:

            case AdsType.AdMob:
                if (interstitial != null)
                {
                    // Create an empty ad request.
                    AdRequest request = new AdRequest.Builder().Build();
                    // Load the interstitial with the request.
                    interstitial.LoadAd(request);
                }
                else
                {
                    Debug.LogError("AdsManager: admob not inted");
                }
                
                break;
            default:
                Debug.LogError("AddsManeger.initInterstitial.switch.default");
                break;
        }

    }


    public static void initInterstitial()
    {
        switch (currentAdsType)
        {
            case AdsType.HeyzApp:
                HeyzapAds.start("heyzaKey", HeyzapAds.FLAG_NO_OPTIONS);
                isInited = true;
                break;
            case AdsType.AdMob:
                interstitial = new InterstitialAd("interestial ad id");
                if (interstitial != null)
                {
                    isInited = true;
                }
                else
                {
                    Debug.LogError("AdsManager: interestial ad object is NULL. Admob not inited");
                }

                break;
        }
    }

    public static void showAds()
    {
        switch (currentAdsType)
        {
            case AdsType.HeyzApp:
                if (HZInterstitialAd.isAvailable())
                {
                    HZInterstitialAd.show();
                }
                break;
            case AdsType.AdMob:
                if (interstitial != null)
                {
                    if (interstitial.IsLoaded())
                    {
                        interstitial.Show();
                    }
                }
                else
                {
                    Debug.LogError("AdsManager: admob not inited");
                }
                
                break;
        }


    }


    
    public static AdsType CurrentAddsType
    {
        get
        {
            return currentAdsType;
        }
        set
        {
            currentAdsType = value;
        }
    }
}
