using System.Collections;
using UnityEngine;

public class OldManDialog6 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz
    AudioManager audioManager;

    //silinecekler
    public GameObject oldPeople, newPeople;
    public GameObject cameraa, playerr, corpses, smell;
    public Animator a;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng         

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;
        PlayerPrefs.SetString("level6kabulettimi", "sýfýr");//evet

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
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
            case 26:
                switch (butonNo)
                {
                    case 1:
                        easyText[3] = dialog[0 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                        speecCountOut = 27;
                        PlayerPrefs.SetString("level6kabulettimi", "evet");
                        break;

                    case 2:
                        easyText[4] = dialog[2 * totalLanguage + currentLanguage];
                        speecCountOut = 49;
                        break;
                }
                break;

            case 51:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 52;
                        break;

                    case 2:
                        easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                        speecCountOut = 51;
                        break;
                }
                break;

            case 52:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage]; 
                        speecCountOut = 74;
                        break;

                    case 2:
                        easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                        speecCountOut = 52;
                        break;
                }
                break;

            case 76:
                switch (butonNo)
                {
                    case 1:
                        easyText[3] = dialog[7 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[8 * totalLanguage + currentLanguage];
                        speecCountOut = 77;
                        break;

                    case 2:
                        easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                        speecCountOut = 74;
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
                easyText[3] = dialog[10 * totalLanguage + currentLanguage];
                easyText[4] = dialog[11 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[4] = dialog[12 * totalLanguage + currentLanguage];
                easyText[3] = dialog[13 * totalLanguage + currentLanguage];
                speecCountOut = 2;
                npcTalkFirst = true;
                break;

            case 2:
                easyText[4] = dialog[14 * totalLanguage + currentLanguage];
                easyText[3] = dialog[15 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                npcTalkFirst = true;
                break;

            case 3:
                easyText[4] = dialog[16 * totalLanguage + currentLanguage];
                easyText[3] = dialog[17 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = true;
                break;

            case 4:
                easyText[4] = dialog[18 * totalLanguage + currentLanguage];
                easyText[3] = dialog[19 * totalLanguage + currentLanguage];
                speecCountOut = 26;
                npcTalkFirst = true;
                break;

            case 26:
                easyText[0] = dialog[20 * totalLanguage + currentLanguage];
                easyText[1] = dialog[21 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 27:
                easyText[3] = dialog[22 * totalLanguage + currentLanguage];
                easyText[4] = dialog[23 * totalLanguage + currentLanguage];
                speecCountOut = 28;
                npcTalkFirst = false;
                break;

            case 28:
                easyText[3] = dialog[24 * totalLanguage + currentLanguage];
                easyText[4] = dialog[25 * totalLanguage + currentLanguage];
                speecCountOut = 51;
                npcTalkFirst = false;
                break;

            case 51:
                easyText[0] = dialog[26 * totalLanguage + currentLanguage];
                easyText[1] = dialog[27 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 52:
                easyText[0] = dialog[28 * totalLanguage + currentLanguage];
                easyText[1] = dialog[29 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 76:
                easyText[0] = dialog[30 * totalLanguage + currentLanguage];
                easyText[1] = null;
                easyText[2] = null;
                break;

            case 77:
                easyText[3] = dialog[31 * totalLanguage + currentLanguage];
                easyText[4] = dialog[32 * totalLanguage + currentLanguage];
                speecCountOut = 78;
                npcTalkFirst = false;
                break;

            case 78:
                easyText[3] = "";
                easyText[4] = dialog[33 * totalLanguage + currentLanguage];
                speecCountOut = 79;
                npcTalkFirst = true;
                break;

            case 79:
                easyText[3] = "";
                easyText[4] = dialog[34 * totalLanguage + currentLanguage];
                speecCountOut = 80;
                npcTalkFirst = true;
                break;

            case 80:
                easyText[3] = dialog[35 * totalLanguage + currentLanguage];
                easyText[4] = dialog[36 * totalLanguage + currentLanguage];
                speecCountOut = 100;
                StartCoroutine(Level6Peace());
                npcTalkFirst = true;
                break;

            case 49:
                GetComponent<TalkWithNPC6_OldMan>().FinishedTalk();
                break;
            case 74:
                GetComponent<TalkWithNPC6_OldMan>().FinishedTalk();
                break;
        }
    }

    IEnumerator Level6Peace()
    {
        yield return new WaitForSeconds(8f);
        a.Play("OBFinish");
        yield return new WaitForSeconds(0.4f);
        newPeople.SetActive(true);
        corpses.SetActive(false);
        smell.SetActive(false);
        cameraa.GetComponent<CameraManager>().cameraSpeed = 1;
        playerr.GetComponent<Transform>().position = new Vector3(199.2f, 3.62f);
        
        yield return new WaitForSeconds(1.5f);
        a.Play("OBStart");
        cameraa.GetComponent<CameraManager>().cameraSpeed = 0.1f;
        oldPeople.SetActive(false);
    }
}