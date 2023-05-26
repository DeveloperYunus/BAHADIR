using UnityEngine;

public class _1ZerlikTalk : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    public string[] easyText = new string[5];
    public int speecCountOut;
    [HideInInspector]
    public bool npcTalkFirst;
    
    AudioManager audioManager;
    
    public TextAsset csv;
    string[] dialog;
    [HideInInspector]public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;
        PlayerPrefs.SetInt("totalLanguage", 2);

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] {"@", "\n" }, System.StringSplitOptions.None);
    }
    public void FirsWords()
    {
        easyText[0] = dialog[0 * totalLanguage + currentLanguage];
        easyText[1] = dialog[1 * totalLanguage + currentLanguage];
        easyText[2] = dialog[2 * totalLanguage + currentLanguage];
    }
    
    public void EB(int butonNo, int speecCount)//NPC nin verdiði cevaplar
    {
        audioManager.playSound("talkBtn");
        switch (speecCount)
        {
            case 0:
                GetComponent<TalkWithNPC>().firstTime = false;
                GetComponent<TalkWithNPC>().Bir();
                switch(butonNo)
                {
                    case 1:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 1;
                        break;
                    case 2:
                        easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                        speecCountOut = 1;
                        break;
                    case 3:
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
                        break;
                }
                break;

            case 1:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
                        break;
                    case 2:
                        easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
                        break;                    
                }
                break;

            case 2:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[8 * totalLanguage + currentLanguage];
                        speecCountOut = 3;
                        break;
                    case 2:
                        easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                        speecCountOut = 3;
                        break;
                }
                break;

            case 6:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[10 * totalLanguage + currentLanguage];
                        speecCountOut = 7;
                        break;
                    case 2:
                        easyText[4] = dialog[11 * totalLanguage + currentLanguage];
                        speecCountOut = 9;
                        break;
                }
                break;

            case 25:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[12 * totalLanguage + currentLanguage];
                        speecCountOut = 100;
                        GetComponent<Achievement1>().YouAccept();
                        break;
                    case 2:
                        easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                        speecCountOut = 27;
                        break;
                }
                break;
        }
    }
    public void ForwardSpeech(int speecCount)//Sorular ve karþýlýklý konusmalar
    {
        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 1:
                easyText[0] = dialog[14 * totalLanguage + currentLanguage];
                easyText[1] = dialog[15 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 2:
                easyText[0] = dialog[16 * totalLanguage + currentLanguage];
                easyText[1] = dialog[17 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 3:
                easyText[3] = dialog[18 * totalLanguage + currentLanguage];
                easyText[4] = dialog[19 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = false;
                break;

            case 4:
                easyText[3] = dialog[20 * totalLanguage + currentLanguage];
                easyText[4] = null;
                speecCountOut = 5;
                npcTalkFirst = false;
                break;

            case 5:
                easyText[3] = dialog[21 * totalLanguage + currentLanguage];
                easyText[4] = dialog[22 * totalLanguage + currentLanguage];
                speecCountOut = 6;
                npcTalkFirst = false;
                break;

            case 6:
                easyText[0] = dialog[23 * totalLanguage + currentLanguage];
                easyText[1] = dialog[24 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 7:
                easyText[3] = dialog[25 * totalLanguage + currentLanguage];
                easyText[4] = dialog[26 * totalLanguage + currentLanguage];
                speecCountOut = 8;
                npcTalkFirst = false;
                break;

            case 8:
                easyText[3] = dialog[27 * totalLanguage + currentLanguage];
                easyText[4] = dialog[28 * totalLanguage + currentLanguage];
                speecCountOut = 9;
                npcTalkFirst = false;
                break;

            case 9:
                easyText[3] = dialog[29 * totalLanguage + currentLanguage];
                easyText[4] = dialog[30 * totalLanguage + currentLanguage];
                speecCountOut = 25;
                npcTalkFirst = true;
                GetComponent<Achievement1>().ZerlikGiveSword();
                break;

            case 25:
                easyText[0] = dialog[31 * totalLanguage + currentLanguage];
                easyText[1] = dialog[32 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 27:
                easyText[3] = dialog[33 * totalLanguage + currentLanguage];
                easyText[4] = "";
                speecCountOut = 28;
                npcTalkFirst = false;
                PlayerPrefs.SetInt("1.ilk.konusma", 1);
                break;

            case 28:
                easyText[3] = dialog[34 * totalLanguage + currentLanguage];
                easyText[4] = "";
                speecCountOut = 29;
                npcTalkFirst = false;
                break;

            case 29:
                easyText[3] = "";
                easyText[4] = "";
                speecCountOut = 25;
                npcTalkFirst = false;
                GetComponent<TalkWithNPC>().FinishedTalk();
                break;
        }
    }
}