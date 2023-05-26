using UnityEngine;

public class Trader1Dialog6 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz
    AudioManager audioManager;

    //silinecekler buranýn altýnda yazýlacak
    [HideInInspector] public int threeQuestion;                                   //3 soruyuda sordugu zaman hýc mantýklý deðil der bahadýr.

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;
        threeQuestion = 0;
        PlayerPrefs.SetString("level6canTalkDremarch", "hayýr");

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void FirsWords()
    {
        easyText[0] = dialog[0 * totalLanguage + currentLanguage];
        easyText[1] = dialog[1 * totalLanguage + currentLanguage];
        easyText[2] = null;
    }
    public void EB(int butonNo, int speecCount)//NPC nin verdiði cevaplar
    {
        audioManager.playSound("talkBtn");
        easyText[3] = null;
        switch (speecCount)
        {
            case 0:
                switch (butonNo)
                {
                    case 1:
                        easyText[3] = dialog[2 * totalLanguage + currentLanguage];
                        if (gameObject.name == "Trader1")
                            easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        else
                            easyText[4] = dialog[4 * totalLanguage + currentLanguage];

                        speecCountOut = 1;
                        break;

                    case 2:
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
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

        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                easyText[0] = dialog[6 * totalLanguage + currentLanguage];
                easyText[1] = dialog[7 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 1:
                if (gameObject.name == "Trader1")
                    easyText[3] = dialog[8 * totalLanguage + currentLanguage];
                else
                    easyText[3] = dialog[9 * totalLanguage + currentLanguage];

                if (gameObject.name == "Trader1")
                    easyText[4] = dialog[10 * totalLanguage + currentLanguage];
                else
                    easyText[4] = dialog[11 * totalLanguage + currentLanguage];

                speecCountOut = 100;
                npcTalkFirst = false;
                break;

            case 2:
                easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                PlayerPrefs.SetString("level6canTalkDremarch", "evet");
                npcTalkFirst = false;
                break;

            case 3:
                easyText[3] = dialog[14 * totalLanguage + currentLanguage];
                easyText[4] = dialog[15 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = false;
                break;

            case 4:
                easyText[3] = dialog[16 * totalLanguage + currentLanguage];
                easyText[4] = dialog[17 * totalLanguage + currentLanguage];
                speecCountOut = 100;
                npcTalkFirst = false;
                break;          
        }
    }

 
}