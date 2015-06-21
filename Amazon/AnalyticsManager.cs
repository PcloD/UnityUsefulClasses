using Amazon.CognitoIdentity;
using Amazon.MobileAnalytics.MobileAnalyticsManager;
using Amazon.Util.Internal;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{

    public string IdentityPoolId    = "Identy pool";
    public string AppId             = "App ID";


    private static AnalyticsManager instance;
    public  static AnalyticsManager Get
    {
        get { return instance; }
    }

    private MobileAnalyticsManager analyticsManager;
    private CognitoAWSCredentials _credentials;

    //========================================================

    #region Unity Methods
    void Awake () 
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            Debug.LogError("Another instance of AnalyticsManager has been destroyed");
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        //==================================
        setDebugData();

        initCognitoAndAnalytics();
	}

    void OnApplicationFocus(bool focus)
    {
        if (analyticsManager != null)
        {
            if (focus)
            {
                analyticsManager.ResumeSession();
            }
            else
            {
                analyticsManager.PauseSession();
            }
        }
    }

    #endregion

    #region Private

    private void setDebugData()
    {
#if UNITY_EDITOR
        /// This is just to spoof the application to think that its running on iOS platform
        AmazonHookedPlatformInfo.Instance.Platform          = "iPhoneOS";
        AmazonHookedPlatformInfo.Instance.Model             = "iPhone";
        AmazonHookedPlatformInfo.Instance.Make              = "Apple";
        AmazonHookedPlatformInfo.Instance.Locale            = "en_US";
        AmazonHookedPlatformInfo.Instance.PlatformVersion   = "8.1.2";

        AmazonHookedPlatformInfo.Instance.Title             = "Analytics Test App";
        AmazonHookedPlatformInfo.Instance.VersionName       = "v1.0";
        AmazonHookedPlatformInfo.Instance.VersionCode       = "1.0";
        AmazonHookedPlatformInfo.Instance.PackageName       = "com.area730.testapp";
#endif
    }

    private void initCognitoAndAnalytics()
    {
        _credentials        = new CognitoAWSCredentials(IdentityPoolId, Amazon.RegionEndpoint.USEast1);
        analyticsManager    = MobileAnalyticsManager.GetOrCreateInstance(_credentials, Amazon.RegionEndpoint.USEast1, AppId);
    }

    #endregion

    #region Interface

    public void RecordEvent(CustomEvent customEvent)
    {
        analyticsManager.RecordEvent(customEvent);
    }

    public void RecordEvent(AmazonEventBuilder eventBuilder)
    {
        analyticsManager.RecordEvent(eventBuilder.GetEvent()); 
    }

    void OnDestroy()
    {
        Debug.LogError("Analytics manager SINGLETON has been destroyed");
    }

    #endregion

}
