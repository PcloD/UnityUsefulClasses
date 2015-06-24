using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

/// <summary>
/// This class static provides api to use Heyzapp and Admob monetization systems
/// </summary>
public static class AdsManager
{
    public enum 	AdsType { HeyzApp, AdMob };
	public static 	AdsType currentAdsType 	= AdsType.HeyzApp;

	public const string heyzappKey 			= "heyzappKey";
	public const string admobKey 			= "ca-app-pub-xxxxxxxxxxx/xxxxxxxxx";

	private static bool isHeyzappInited		= false;
  
    private static InterstitialAd interstitial;//admob use only

	/// <summary>
	/// Inits the interstitial, depending on choosen type, by default is HeyzApp
	/// </summary>
	public static void initInterstitial()
	{
		switch (currentAdsType)
		{
		case AdsType.HeyzApp:
			HeyzapAds.start(heyzappKey, HeyzapAds.FLAG_NO_OPTIONS);
			isHeyzappInited = true;
			break;
			
		case AdsType.AdMob:
			interstitial = new InterstitialAd(admobKey);
			break;
		}
	}


	/// <summary>
	/// Use for fetching admob only
	/// </summary>
    public static void fetchInterstitial()
    {      
		if (interstitial != null)
		{
			AdRequest request = new AdRequest.Builder().Build();
			interstitial.LoadAd(request);
 		}
 		else
		{
			Debug.LogError("AdsManager: admob interstitial  is not inited. Please init it explicit!");
		}   
    }
	/// <summary>
	/// Shows adds depends on currentAdsType
	/// </summary>
    public static void showAds()
    {
        switch (currentAdsType)
        {
            case AdsType.HeyzApp:
				if(isHeyzappInited)
				{
	                if (HZInterstitialAd.isAvailable())
	                {
	                    HZInterstitialAd.show();
	                }
	                
				}
				else
				{
					Debug.LogWarning("AdsManager: init Heyzapp from showAds");
					initInterstitial();
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
	/// <summary>
	/// This methods shows ads, after showing - change type of monetization system
	/// 
	/// </summary>
	public static void showAndSwitchAds()
	{
		switch (currentAdsType)
		{
		case AdsType.HeyzApp:
			if(isHeyzappInited)
			{
				if (HZInterstitialAd.isAvailable())
				{
					HZInterstitialAd.show();
					currentAdsType = AdsType.AdMob;
				}

			}
			else
			{
				Debug.LogWarning("AdsManager: init Heyzapp from showAndSwitchAds");
				initInterstitial();
			}

			break;
			
		case AdsType.AdMob:
			if (interstitial != null)
			{
				if (interstitial.IsLoaded())
				{
					interstitial.Show();
					currentAdsType = AdsType.HeyzApp;
				}
			}
			else
			{
				Debug.LogWarning("AdsManager: admob not inited. But we init this after this try");
				initInterstitial();
			}
			
			break;
		}
	}
    
}
