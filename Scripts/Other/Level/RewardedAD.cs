using UnityEngine;
using UnityEngine.Advertisements;
using System;
using DG.Tweening;
using TMPro;

public class RewardedAD : MonoBehaviour, IUnityAdsListener
{
    public bool testMode = true;
    public string androidID = "4646587";            //4646587
    public string iosID = "4646586";                //4646586

    string rewardedID = "Rewarded_Android";

    public EnvanterButton eb;
    public SaveLoad sl;
    public TextMeshProUGUI AdTimeText;
    public int reklamBekelmeDK;
    float zaman, zaman2;
    int forAdTime;
    bool canWacthAd;

    void Start()
    {
        Advertisement.AddListener(this);
        LoadAd();

        //PlayerPrefs.SetInt("hour", 0);
        //PlayerPrefs.SetInt("minute", 0);
        //PlayerPrefs.SetInt("second", 0);
        canWacthAd = false;
        forAdTime = PlayerPrefs.GetInt("hour") * 3600 + PlayerPrefs.GetInt("minute") * 60 + PlayerPrefs.GetInt("second");
    }
    private void Update()
    {
        if (Time.time > zaman && !Advertisement.isInitialized)
        {
            zaman = Time.time + 0.5f;
            LoadAd();
        }

        if (Time.time > zaman2)
        {
            zaman2 = Time.time + 1;
            int geriSayim = 0;
            int saniyeCinsindenFark;
            bool geriSayimVarmi = false;
            saniyeCinsindenFark = (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) - forAdTime;

            if (saniyeCinsindenFark > reklamBekelmeDK * 60)
            {
                //saniye cinsinden aralarýnda fark bekleme süresinden fazlamý
                AdTimeText.text = "00:00";
                canWacthAd = true;
            }
            else
            {
                geriSayimVarmi = true;
                geriSayim = reklamBekelmeDK * 60 - saniyeCinsindenFark;
            }

            if (geriSayimVarmi && geriSayim > 0)
            {
                canWacthAd = false;
                if (geriSayim > 60)
                {
                    int a = geriSayim / 60;
                    AdTimeText.text = a + ":" + (geriSayim - a * 60);
                }
                else
                {
                    if (geriSayim < 10)
                        AdTimeText.text = "00:0" + geriSayim;
                    else
                        AdTimeText.text = "00:" + geriSayim;
                }
            }
            else
            {
                canWacthAd = true;
            }
        }
    }

    void LoadAd()
    {       
#if UNITY_ANDROID
        Advertisement.Initialize(androidID, testMode);
#else
        Advertisement.Initialize(iosID, testMode);
#endif        
    }
    public void ShowRevarded()
    {
        if((PlayerPrefs.GetInt("day") != DateTime.Now.Day | canWacthAd) && Advertisement.IsReady(rewardedID))
        {
            Advertisement.Show(rewardedID);

            canWacthAd = false;
            PlayerPrefs.SetInt("hour", DateTime.Now.Hour);
            PlayerPrefs.SetInt("minute", DateTime.Now.Minute);
            PlayerPrefs.SetInt("second", DateTime.Now.Second);
            PlayerPrefs.SetInt("day", DateTime.Now.Day);
            forAdTime = PlayerPrefs.GetInt("hour") * 3600 + PlayerPrefs.GetInt("minute") * 60 + PlayerPrefs.GetInt("second");
        }
        else
        {
            AdTimeText.DOKill();
            AdTimeText.DOFade(1, 0.6f);
            AdTimeText.DOFade(0, 1f).SetDelay(3f);
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
        if(placementId == rewardedID)
        {
            if(showResult == ShowResult.Finished)
            {
                eb.ourCoin += 20;
                sl.SaveItem();
                Advertisement.RemoveListener(this);
            }
        }
    }

    public void OnDestroy()//baska bir sahneye gidince listener i yok eder
    {
        Advertisement.RemoveListener(this);
    }
}
