using System;
using UnityEngine;

public class MainMenuResetData : MonoBehaviour
{
    Skill dataHolder;
    AudioManager audioManager;

    void Start()
    {
        if (PlayerPrefs.GetString("nextLevelName") == "1")
        {
            LevelTransition.instance.GoLevelBasicForEnum("1", 0f);
        }
        else if (PlayerPrefs.GetString("nextLevelName") == "2")
        {
            LevelTransition.instance.GoLevelBasicForEnum("2", 0f);
        }
        else if (PlayerPrefs.HasKey("ilkAcilis"))
        {
            LevelTransition.instance.GoLevelBasicForEnum("BaseOfOperation", 0f);
        }

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        ResetData();
    }

    void ResetData()
    {
        if (!PlayerPrefs.HasKey("ilkAcilis"))
        {
            dataHolder = GetComponent<SaveLoad>().playerData;
            PlayerPrefs.SetInt("resetGame", 1);
            PlayerPrefs.SetInt("language", 1);                                  //baþlangýcta ve sýfýrlamalarda dil ingilizce olsun

            dataHolder.can = 0;
            dataHolder.enerji = 0;
            dataHolder.zýrh = 0;
            dataHolder.buyuHasari = 0;
            dataHolder.fizikselHasar = 0;
            dataHolder.canCalma = 0;
            dataHolder.skillLevel = 0;

            dataHolder.canYenilenmesi = 0;
            dataHolder.canYenilenmesiPasif = 0;
            dataHolder.enerjiYenilenmesi = 0;
            dataHolder.enerjiYenilenmesiPasif = 0;

            GetComponent<SaveLoad>().SaveSkill();
            GetComponent<SaveLoad>().SaveResetItem();
            print("reset skill maýn menu");
        }
    }
    public void MainMenuGoLevel1(string sceneName)
    {
        audioManager.playSound("button1");
        GameObject.Find("LevelLoaderFade").GetComponent<Animator>().SetTrigger("Start");
        PlayerPrefs.SetString("ilkAcilis", "2.giriþ");
        PlayerPrefs.SetString("nextLevelName", "1");
        LevelTransition.instance.GoLevelBasicForEnum(sceneName, 1f);
    }
}
