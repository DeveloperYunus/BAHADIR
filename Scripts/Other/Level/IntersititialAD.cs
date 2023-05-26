using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class IntersititialAD : MonoBehaviour, IUnityAdsListener
{
    public bool testMode = true;
    public string androidID = "4646587";
    public string iosID = "4646586";

    string interstitialID = "Interstitial_Android";

    void Start()
    {
        Advertisement.AddListener(this);

#if UNITY_ANDROID
        Advertisement.Initialize(androidID, testMode);
#else
        Advertisement.Initialize(iosID, testMode);
#endif
    }

    public void ShowInterstitial()
    {
        if (Advertisement.IsReady(interstitialID))// && int.Parse(PlayerPrefs.GetString("nextLevelName")) %2 == 0  her iki bölümde bir geçiþ reklamý gösterir.(þimdilik kapalý)
        {
            Advertisement.Show(interstitialID);
        }
        else
        {
            GameManager.GameM.Go1();
            Advertisement.RemoveListener(this);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
    public void OnUnityAdsDidError(string message)
    {
    }
    public void OnUnityAdsDidStart(string placementId)
    {
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == interstitialID)
        {
            GameManager.GameM.Go1();
            Advertisement.RemoveListener(this);
        }
    }

    public void OnDestroy()//baska bir sahneye gidince listener i yok eder
    {
        Advertisement.RemoveListener(this);
    }
}
