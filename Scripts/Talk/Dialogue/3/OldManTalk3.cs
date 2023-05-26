using System.Collections;
using UnityEngine;

public class OldManTalk3 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz
    AudioManager audioManager;

    //silinecekler buranýn altýnda yazýlacak
    int whichCount;                                                               //whichSpeecCountBeforeQuestion
    [HideInInspector] public int threeQuestion;                                   //3 soruyuda sordugu zaman hýc mantýklý deðil der bahadýr.
    public EnvanterButton eb;

    public TextAsset csv;
    [HideInInspector] public string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;
        whichCount = 0;
        threeQuestion = 0;
        PlayerPrefs.SetString("level3Secim", "sýfýrla");

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void FirsWords()
    {
        easyText[0] = null;
        easyText[1] = null;
        easyText[2] = null;
    }
    public void EB(int butonNo, int speecCount)//NPC nin verdiði cevaplar
    {
        audioManager.playSound("talkBtn");
        easyText[3] = null;
        switch (speecCount)
        {
            case 1:
                switch(butonNo)
                {
                    case 1:
                        easyText[4] = dialog[0 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
                        break;
                    case 2:
                        easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                        speecCountOut = 3;
                        break;                    
                }
                break;

            case 2:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[2 * totalLanguage + currentLanguage];
                        speecCountOut = 100;                        
                        break;
                    case 2:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 4;
                        break;
                    case 3:
                        easyText[3] = null;
                        whichCount = 2;
                        speecCountOut = 10;
                        StartCoroutine(Level3Wait());
                        break;
                }
                break;

            case 3:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                        speecCountOut = 100;
                        break;
                    case 2:
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                        speecCountOut = 4;
                        break;
                    case 3:
                        easyText[3] = null;
                        whichCount = 3;
                        speecCountOut = 10;
                        StartCoroutine(Level3Wait());
                        break;
                }
                break;

            case 4:
                switch (butonNo)
                {
                    case 1:
                        if (eb.ourCoin >= 40)
                        {
                            easyText[3] = dialog[6 * totalLanguage + currentLanguage] + "40" + dialog[33 * totalLanguage + currentLanguage];
                            easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                            speecCountOut = 6;
                            PlayerPrefs.SetString("level3Secim", "yardimEtti");
                            eb.ourCoin -= 40;
                        }
                        else if (eb.ourCoin > 0) 
                        { 
                            easyText[3] = dialog[8 * totalLanguage + currentLanguage] + (eb.ourCoin * -1) + dialog[33 * totalLanguage + currentLanguage];
                            easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                            speecCountOut = 6;
                            PlayerPrefs.SetString("level3Secim", "yardimEtti");
                            eb.ourCoin = 0;
                        }
                        else
                        {
                            easyText[4] = dialog[10 * totalLanguage + currentLanguage];
                            speecCountOut = 6;
                            PlayerPrefs.SetString("level3Secim", "umursamadý");
                            eb.ourCoin = 0;
                        }
                        break;
                    case 2:
                        easyText[3] = dialog[11 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[12 * totalLanguage + currentLanguage];
                        speecCountOut = 7;
                        PlayerPrefs.SetString("level3Secim", "yalanSoyledi");
                        eb.ourCoin += 5;
                        break;
                    case 3:
                        easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                        speecCountOut = 7;
                        PlayerPrefs.SetString("level3Secim", "umursamadý");
                        break;
                }
                break;

            case 6:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[14 * totalLanguage + currentLanguage];
                        speecCountOut = 100;
                        break;
                    case 2:
                        easyText[3] = dialog[15 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[16 * totalLanguage + currentLanguage];
                        PlayerPrefs.SetString("level3Secim", "Savaþýn");
                        speecCountOut = 8;
                        break;
                }
                break;

            case 10:
                stringForOut = dialog[17 * totalLanguage + currentLanguage];
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[49 * totalLanguage + currentLanguage];              //...Osa'dayýz.
                        speecCountOut = whichCount;
                        threeQuestion++;
                        break;
                    case 2:
                        easyText[4] = dialog[50 * totalLanguage + currentLanguage];
                        speecCountOut = whichCount;
                        threeQuestion++;
                        break;
                    case 3:
                        easyText[4] = dialog[51 * totalLanguage + currentLanguage];
                        speecCountOut = whichCount;
                        threeQuestion++;
                        break;
                }
                break;
        }
    }
    public void ForwardSpeech(int speecCount)//Sorular ve karþýlýklý konusmalar
    {
        /*  easyText[0] = "Ýçeriði ne?";
            easyText[1] = "Dolandýrýlmak istemiyorum saðol.";
            easyText[2] = null;

            easyText[3] = // onemli not bu null olamaz, olursa forwardspeech deki ilk if in içine girmiyor kod
            easyText[4] = "...Hoþbulduk";
            speecCountOut = 1;
            npcTalkFirst = true;    */

        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                easyText[3] = "";
                easyText[4] = dialog[18 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[0] = dialog[19 * totalLanguage + currentLanguage];
                easyText[1] = dialog[20 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 2:
                easyText[0] = dialog[21 * totalLanguage + currentLanguage];
                easyText[1] = dialog[22 * totalLanguage + currentLanguage];
                easyText[2] = dialog[23 * totalLanguage + currentLanguage];
                break;

            case 3:
                easyText[0] = dialog[24 * totalLanguage + currentLanguage];
                easyText[1] = dialog[25 * totalLanguage + currentLanguage];
                easyText[2] = dialog[26 * totalLanguage + currentLanguage];
                break;

            case 4:
                if (PlayerPrefs.GetString("kiliciVerdimi") == "verdi") //2. bolumde kýlýcýný verýp vermediðini kontrol etmek için
                {
                    easyText[3] = dialog[27 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[28 * totalLanguage + currentLanguage];
                    speecCountOut = 5;
                    npcTalkFirst = false;
                    PlayerPrefs.SetString("level3Secim", "kiliciVermisti");//oyun sonundaký hýkeye anlatýmýnda kullanýlacak olan kayýtlarý tutmak ýcýn
                }
                else
                {
                    if (eb.ourCoin >= 40)
                    {
                        easyText[0] = dialog[29 * totalLanguage + currentLanguage];
                        easyText[1] = dialog[30 * totalLanguage + currentLanguage];
                        easyText[2] = dialog[31 * totalLanguage + currentLanguage];
                    }
                    else if (eb.ourCoin > 0)
                    {
                        easyText[0] = dialog[32 * totalLanguage + currentLanguage] + (eb.ourCoin*-1) + dialog[33 * totalLanguage + currentLanguage];
                        easyText[1] = dialog[34 * totalLanguage + currentLanguage];
                        easyText[2] = dialog[35 * totalLanguage + currentLanguage];
                    }
                    else
                    {
                        easyText[0] = dialog[36 * totalLanguage + currentLanguage];
                        easyText[1] = dialog[37 * totalLanguage + currentLanguage];
                        easyText[2] = dialog[38 * totalLanguage + currentLanguage];
                    }
                }               
                break;

            case 5:
                easyText[3] = dialog[39 * totalLanguage + currentLanguage];
                easyText[4] = dialog[40 * totalLanguage + currentLanguage];
                speecCountOut = 100;
                npcTalkFirst = false;
                break;

            case 6:
                easyText[0] = dialog[41 * totalLanguage + currentLanguage];
                easyText[1] = dialog[42 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 7:
                easyText[3] = dialog[43 * totalLanguage + currentLanguage];
                easyText[4] = dialog[44 * totalLanguage + currentLanguage];
                speecCountOut = 100;
                npcTalkFirst = false;
                break;

            case 8:
                easyText[3] = dialog[45 * totalLanguage + currentLanguage];
                easyText[4] = dialog[46 * totalLanguage + currentLanguage];
                speecCountOut = 100;
                npcTalkFirst = false;
                break;

            case 10:
                easyText[0] = dialog[47 * totalLanguage + currentLanguage];
                easyText[1] = dialog[48 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;
        }
    }
    //silinecek
    IEnumerator Level3Wait()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<TalkWithNPC3>().canIPassTalk = true;
        GetComponent<TalkWithNPC3>().ForwardSpeech();
    }
}