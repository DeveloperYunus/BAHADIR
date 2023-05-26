using System.Collections;
using UnityEngine;

public class DremachDialog5 : MonoBehaviour
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
    bool oneTime;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    private void Start()
    {
        oneTime = true;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;
        whichCount = 0;
        threeQuestion = 0;
        PlayerPrefs.SetString("5KillWitchElection", "sýfýr");
        PlayerPrefs.SetString("ÝsTalkWoman", "sýfýr");
        PlayerPrefs.SetString("MageIsDead", "sýfýr");
        PlayerPrefs.SetString("womanTalk2", "sýfýr");

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void FirsWords()
    {
        if (PlayerPrefs.GetString("MageIsDead") == "yes")
        {
            easyText[0] = dialog[0 * totalLanguage + currentLanguage];
            easyText[1] = null; 
            easyText[2] = null;
        }
        else
        {
            easyText[0] = dialog[1 * totalLanguage + currentLanguage];
            easyText[1] = null;
            easyText[2] = null;
        }
        if (oneTime && PlayerPrefs.GetString("MageIsDead") == "yes")
        {
            oneTime = false;
            speecCountOut = 26;
        }
    }
    public void EB(int butonNo, int speecCount)//NPC nin verdiði cevaplar
    {
        audioManager.playSound("talkBtn");
        easyText[3] = null;
        switch (speecCount)
        {
            case 0:
                switch(butonNo)
                {
                    case 1:
                        easyText[3] = dialog[2 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 1;
                        break;                    
                }
                break;

            case 4:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                        speecCountOut = 5;                        
                        break;

                    case 2:
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                        speecCountOut = 24;
                        break;

                    case 3:
                        easyText[3] = null;
                        whichCount = speecCountOut;
                        speecCountOut = 10;
                        StartCoroutine(Level3Wait());
                        break;
                }
                break;

            case 5:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                        speecCountOut = 25;
                        PlayerPrefs.SetString("5KillWitchElection", "yes");
                        PlayerPrefs.SetString("ÝsTalkWoman", "yes");
                        break;

                    case 2:
                        easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                        speecCountOut = 25;
                        PlayerPrefs.SetString("5KillWitchElection", "yesWhitoutMoney");
                        PlayerPrefs.SetString("ÝsTalkWoman", "yes");
                        break;
                }
                break;

            case 10:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[8 * totalLanguage + currentLanguage];
                        speecCountOut = whichCount;
                        break;

                    case 2:
                        easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                        speecCountOut = whichCount;
                        break;

                    case 3:
                        easyText[4] = dialog[10 * totalLanguage + currentLanguage];
                        speecCountOut = whichCount;
                        break;
                }
                break;

            case 28:
                switch (butonNo)
                {
                    case 1:
                        easyText[3] = dialog[11 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[12 * totalLanguage + currentLanguage];
                        speecCountOut = 29;    
                        break;

                    case 2:
                        easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                        speecCountOut = 100;                        
                        break;
                }
                break;
        }
    }
    public void ForwardSpeech(int speecCount)//Sorular ve karþýlýklý konusmalar
    {
        /*  
        easyText[0] = "Ýçeriði ne?";
        easyText[1] = "Dolandýrýlmak istemiyorum saðol.";
        easyText[2] = null;

        easyText[3] = // onemli not bu null olamaz, olursa forwardspeech deki ilk if in içine girmiyor kod
        easyText[4] = "...Hoþbulduk";
        speecCountOut = 1;
        npcTalkFirst = true;    
        */

        

        easyText[0] = null; 
        easyText[1] = null;
        easyText[2] = null;
        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                easyText[0] = dialog[14 * totalLanguage + currentLanguage];
                easyText[1] = null;
                easyText[2] = null;
                break;

            case 1:
                easyText[3] = dialog[15 * totalLanguage + currentLanguage];
                easyText[4] = dialog[16 * totalLanguage + currentLanguage];
                speecCountOut = 2;
                npcTalkFirst = false;
                break;

            case 2:
                easyText[3] = dialog[17 * totalLanguage + currentLanguage];
                easyText[4] = dialog[18 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                npcTalkFirst = false;
                break;

            case 3:
                easyText[3] = dialog[19 * totalLanguage + currentLanguage];
                easyText[4] = dialog[20 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = false;
                break;

            case 4:
                easyText[0] = dialog[21 * totalLanguage + currentLanguage];
                easyText[1] = dialog[22 * totalLanguage + currentLanguage];
                easyText[2] = dialog[23 * totalLanguage + currentLanguage];
                break;

            case 5:
                easyText[0] = dialog[24 * totalLanguage + currentLanguage];
                easyText[1] = dialog[25 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 10:
                easyText[0] = dialog[26 * totalLanguage + currentLanguage];
                easyText[1] = dialog[27 * totalLanguage + currentLanguage];
                easyText[2] = dialog[28 * totalLanguage + currentLanguage];
                break;

            case 24:
                GetComponent<TalkWithNPC5_Dremach>().FinishedTalk();
                break;

            case 25:
                GetComponent<TalkWithNPC5_Dremach>().FinishedTalk();
                break;

            case 26:
                easyText[3] = dialog[29 * totalLanguage + currentLanguage];
                easyText[4] = dialog[30 * totalLanguage + currentLanguage];
                PlayerPrefs.SetString("womanTalk2", "yes");
                speecCountOut = 27;
                npcTalkFirst = true;
                break;

            case 27:
                easyText[3] = dialog[31 * totalLanguage + currentLanguage];
                easyText[4] = dialog[32 * totalLanguage + currentLanguage];
                if (PlayerPrefs.GetString("5KillWitchElection") == "yes")
                {
                    easyText[4] += dialog[33 * totalLanguage + currentLanguage];
                    GetComponent<DropGold>().eb.ourCoin += 30;
                }
                speecCountOut = 28;
                npcTalkFirst = true;
                break;
            
            case 28:
                easyText[0] = dialog[34 * totalLanguage + currentLanguage];
                easyText[1] = dialog[35 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 29:
                easyText[3] = dialog[36 * totalLanguage + currentLanguage];
                easyText[4] = dialog[37 * totalLanguage + currentLanguage];
                speecCountOut = 100;
                npcTalkFirst = false;
                break;
        }
    }


    //eb kýsmýnda yený bir eb penceresi acar
    IEnumerator Level3Wait()//5 bölumde gerekli bu
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<TalkWithNPC5_Dremach>().canIPassTalk = true;
        GetComponent<TalkWithNPC5_Dremach>().ForwardSpeech();
    }    
}