using UnityEngine;

public class KuryeDialog6 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz
    AudioManager audioManager;

    public OldManDialog6 oldManScript;
    
    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng


    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);            //dialog[1 * totalLanguage + currentLanguage];
    }

    public void FirsWords()
    {
        easyText[0] = dialog[0 * totalLanguage + currentLanguage];
        easyText[1] = null;
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
                        easyText[3] = dialog[1 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[2 * totalLanguage + currentLanguage];
                        speecCountOut = 0;
                        break;

                    case 2:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 1;
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
                easyText[3] = dialog[4 * totalLanguage + currentLanguage];
                easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[3] = dialog[6 * totalLanguage + currentLanguage];
                easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                speecCountOut = 2;
                npcTalkFirst = true;
                break;

            case 2:
                easyText[3] = dialog[8 * totalLanguage + currentLanguage];
                easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                npcTalkFirst = true;
                break;

            case 3:
                easyText[3] = dialog[10 * totalLanguage + currentLanguage];
                easyText[4] = dialog[11 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = true;
                break;

            case 4:
                easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                speecCountOut = 5;
                npcTalkFirst = true;
                break;

            case 5:
                easyText[3] = dialog[14 * totalLanguage + currentLanguage];
                easyText[4] = "";
                speecCountOut = 6;
                npcTalkFirst = false;

                GetComponent<Animator>().Play("Dead");
                break;

            case 6:
                easyText[3] = dialog[15 * totalLanguage + currentLanguage];
                easyText[4] = "";
                speecCountOut = 100;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                GetComponents<BoxCollider2D>()[0].enabled = false;
                GetComponents<BoxCollider2D>()[1].enabled = false;
                npcTalkFirst = false;
                oldManScript.GetComponent<TalkWithNPC6_OldMan>().speecCount = 76;
                gameObject.layer = 10;
                break;
        }
    }  
}