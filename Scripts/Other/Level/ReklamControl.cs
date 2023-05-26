using UnityEngine;
using System;
using TMPro;
using DG.Tweening;

public class ReklamControl : MonoBehaviour
{
    public EnvanterButton eb;
    public TextMeshProUGUI AdTimeText;
    public int reklamBekelmeDK;

    float zaman;
    int forAdTime;
    bool canWacthAd; 

    void Start()
    {
        //PlayerPrefs.SetInt("hour", 0);
        //PlayerPrefs.SetInt("minute", 0);
        //PlayerPrefs.SetInt("second", 0);
        canWacthAd = false;
        forAdTime = PlayerPrefs.GetInt("hour") * 3600 + PlayerPrefs.GetInt("minute") * 60 + PlayerPrefs.GetInt("second");
    }
    public void CreateAndLoadRewardedAd()
    {

    }
    void Update()
    {
        if(Time.time > zaman /*&& !this.rewardedAd.IsLoaded()           rekþam yüklendimi*/)
        {
            zaman = Time.time + 2;
            this.CreateAndLoadRewardedAd();
        }

        if (Time.time > zaman)
        {
            zaman = Time.time + 1;
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


    public void UserChoseToWatchAd()
    {
        if ((PlayerPrefs.GetInt("day") != DateTime.Now.Day | canWacthAd)  /*this.rewardedAd.IsLoaded()*/)
        {
            //this.rewardedAd.Show();       reklmý göster

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
}