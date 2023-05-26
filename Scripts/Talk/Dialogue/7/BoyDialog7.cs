using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoyDialog7 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz

    //silinecekler
    public EnvanterButton eb;
    public AudioManager audioM;
    public Animator a;
    public GameObject deserttile, desertPit;
    public GameObject forestTile;
    public GameObject peresentEnemy, pastEnemy, calýCýrpý;
    public GameObject forestBG, desertBG;
    public GameObject forestPS, desertPS;
    public GameObject forestEnv, desertEnv, iceGolems;
    public GameObject goldBag;

    bool firstTimeBushPull;
    AudioManager audioManager;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        firstTimeBushPull = true;
        speecCountOut = 0;
        PlayerPrefs.SetString("level7", "sýfýr");
        PlayerPrefs.SetString("L7ParayýAldýmý", "sýfýr");

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);        //dialog[2 * totalLanguage + currentLanguage];
    }

    public void FirsWords()
    {
        easyText[0] = "Muhtar senmisin";
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
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[0 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
                        break;

                    case 2:
                        easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                        speecCountOut = 24;
                        break;
                }
                break;

            case 29:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[2 * totalLanguage + currentLanguage];
                        speecCountOut = 30;
                        break;

                    case 2:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        PlayerPrefs.SetString("L7ParayýAldýmý", "almadý");
                        speecCountOut = 31;
                        eb.ourCoin -= 200;
                        StartCoroutine(Pit(false));
                        audioM.playSound("pitClose");
                        audioM.playSound("pitClose1");
                        audioM.playSound("coinBuy2");
                        goldBag.SetActive(true);
                        break;
                }
                break;

            case 30:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                        PlayerPrefs.SetString("L7ParayýAldýmý", "aldý");
                        speecCountOut = 32;
                        break;

                    case 2:
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                        PlayerPrefs.SetString("L7ParayýAldýmý", "almadý");
                        speecCountOut = 31;
                        eb.ourCoin -= 200;
                        StartCoroutine(Pit(false));
                        audioM.playSound("coinBuy2");
                        audioM.playSound("pitClose");
                        audioM.playSound("pitClose1");
                        goldBag.SetActive(true);
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

        //pitClose
        //pitOpen
        //timeTravel

        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:                
                easyText[3] = "";
                easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[0] = dialog[7 * totalLanguage + currentLanguage];
                easyText[1] = dialog[8 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 2:
                easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                easyText[3] = dialog[10 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                npcTalkFirst = true;
                break;

            case 3:
                if (firstTimeBushPull)//bu olmazsa adam sürekli konusmadan cýkýp girer ve calýya sürekli kuvvet uydulanýr
                {
                    StartCoroutine(Pit(true));
                    audioM.playSound("pitOpen");
                    audioM.playSound("pitOpen1");
                    calýCýrpý.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 200));
                    calýCýrpý.GetComponent<Rigidbody2D>().AddTorque(15);
                    firstTimeBushPull = false;
                }
                easyText[4] = dialog[11 * totalLanguage + currentLanguage];
                easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = false;
                break;

            case 4:
                easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                easyText[3] = dialog[14 * totalLanguage + currentLanguage];
                speecCountOut = 5;
                npcTalkFirst = false;
                
                break;

            case 5:
                eb.ourCoin += 200;
                goldBag.SetActive(false);
                audioM.playSound("coinBuy2");
                easyText[3] = "";
                StartCoroutine(TimeTravel(false));
                speecCountOut = 26;
                GetComponent<TalkWithNPC7_Boy>().FinishedTalk();
                break;

            case 26:
                easyText[3] = dialog[15 * totalLanguage + currentLanguage];
                easyText[4] = dialog[16 * totalLanguage + currentLanguage];
                speecCountOut = 27;
                npcTalkFirst = false;
                break;

            case 27:
                easyText[3] = dialog[17 * totalLanguage + currentLanguage];
                easyText[4] = dialog[18 * totalLanguage + currentLanguage];
                speecCountOut = 28;
                npcTalkFirst = false;
                break;

            case 28:
                easyText[3] = dialog[19 * totalLanguage + currentLanguage];
                easyText[4] = dialog[20 * totalLanguage + currentLanguage];
                speecCountOut = 29;
                npcTalkFirst = false;
                break;

            case 29:
                easyText[0] = dialog[21 * totalLanguage + currentLanguage];
                easyText[1] = dialog[22 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 30:
                easyText[0] = dialog[23 * totalLanguage + currentLanguage];
                easyText[1] = dialog[24 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 31://parayý yanýna almadý
                StartCoroutine(TimeTravel(true));
                GetComponent<TalkWithNPC7_Boy>().FinishedTalk();

                if (OnDestroyIceGolemL7.howManyIceGolemDie >= 2)                
                    speecCountOut = 51;                
                else                
                    speecCountOut = 76;                
                break;

            case 32://parayý yanýna aldý
                StartCoroutine(TimeTravel(true));
                GetComponent<TalkWithNPC7_Boy>().FinishedTalk();
                eb.ourCoin -= 200;

                if (OnDestroyIceGolemL7.howManyIceGolemDie >= 2)                
                    speecCountOut = 51;                
                else                
                    speecCountOut = 76;                
                break;

            case 51:
                PlayerPrefs.SetString("level7", "col");
                easyText[3] = dialog[25 * totalLanguage + currentLanguage];
                easyText[4] = dialog[26 * totalLanguage + currentLanguage];
                speecCountOut = 52;
                npcTalkFirst = false;
                break;

            case 52:
                easyText[3] = dialog[27 * totalLanguage + currentLanguage];
                easyText[4] = dialog[28 * totalLanguage + currentLanguage];
                if (OnDestroyIceGolemL7.howManyIceGolemDie >= 2)
                    speecCountOut = 53;
                else
                    speecCountOut = 76;
                npcTalkFirst = false;
                break;

            case 53:
                if (PlayerPrefs.GetString("L7ParayýAldýmý") == "aldý") 
                {
                    easyText[3] = dialog[29 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[30 * totalLanguage + currentLanguage];
                }
                else
                {
                    easyText[3] = dialog[31 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[32 * totalLanguage + currentLanguage];
                }
                speecCountOut = 54;
                npcTalkFirst = false;
                break;

            case 54:
                if (PlayerPrefs.GetString("L7ParayýAldýmý") == "aldý")
                {
                    easyText[3] = dialog[33 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[34 * totalLanguage + currentLanguage];
                }
                else
                {
                    easyText[3] = dialog[35 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[36 * totalLanguage + currentLanguage];
                }
                speecCountOut = 55;
                npcTalkFirst = false;
                break;

            case 76:
                if (PlayerPrefs.GetString("L7ParayýAldýmý") == "aldý")
                {
                    easyText[3] = dialog[37 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[38 * totalLanguage + currentLanguage];
                }
                else
                {
                    easyText[3] = dialog[39 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[40 * totalLanguage + currentLanguage];
                }
                
                speecCountOut = 77;
                npcTalkFirst = false;
                break;

            case 77:
                if (PlayerPrefs.GetString("L7ParayýAldýmý") == "aldý")
                {
                    easyText[3] = dialog[41 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[42 * totalLanguage + currentLanguage];
                }
                else
                {
                    easyText[3] = dialog[43 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[44 * totalLanguage + currentLanguage];
                }
                
                speecCountOut = 78;
                npcTalkFirst = false;
                break;

            case 24:
                GetComponent<TalkWithNPC7_Boy>().FinishedTalk();
                break;
            case 55:
                GetComponent<TalkWithNPC7_Boy>().FinishedTalk();
                break;
            case 78:
                GetComponent<TalkWithNPC7_Boy>().FinishedTalk();
                break;
        }
    }

    IEnumerator TimeTravel(bool isGoFuture)
    {
        if(isGoFuture)
        {
            audioM.playSound("timeTravel");
            a.Play("timeTravel");
            yield return new WaitForSeconds(1.3f);
            desertPit.SetActive(false);
            goldBag.SetActive(false);
            if (OnDestroyIceGolemL7.howManyIceGolemDie >= 2)
            {
                print("orman aktif");
                forestTile.SetActive(true);
                deserttile.SetActive(false);
                forestBG.SetActive(true);
                forestPS.SetActive(true);
                desertBG.SetActive(false);
                desertPS.SetActive(false);
                forestEnv.SetActive(true);
                desertEnv.SetActive(false);
                iceGolems.SetActive(false);
            }
            else
            {
                StartCoroutine(Pit(false));
            }
            peresentEnemy.SetActive(true);
            pastEnemy.SetActive(false);
        }
        else
        {
            audioM.playSound("timeTravel");
            a.Play("timeTravel");
            yield return new WaitForSeconds(1.3f);
            peresentEnemy.SetActive(false);
            pastEnemy.SetActive(true);         
        }
    }

    IEnumerator Pit(bool isDigging)
    {
        if(!isDigging)//kapatýyoruz
        {
            for (int i = 0; i < 25; i++)
            {
                yield return new WaitForSeconds(0.05f);
                desertPit.GetComponent<Tilemap>().color = new Color(1, 1, 1, 0.04f * i);
            }
        }
        else
        {
            for (int i = 0; i < 25; i++)
            {
                yield return new WaitForSeconds(0.05f);
                desertPit.GetComponent<Tilemap>().color = new Color(1, 1, 1, 0.04f * (24 - i));
            }
        }
    }
}