using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ZerlikDialog9 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz

    //silinecekler
    public GameObject zerlik, kurgen;
    public TextMeshPro playerTMP;
    AudioManager audioManager;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng             

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;

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
                easyText[3] = dialog[0 * totalLanguage + currentLanguage];
                easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[3] = dialog[2 * totalLanguage + currentLanguage];
                easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                speecCountOut = 2;
                npcTalkFirst = true;
                break;

            case 2:
                easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                easyText[3] = dialog[5 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                npcTalkFirst = true;
                break;

            case 3:
                easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                easyText[3] = dialog[7 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = true;
                break;

            case 4:
                easyText[4] = dialog[8 * totalLanguage + currentLanguage];
                easyText[3] = dialog[9 * totalLanguage + currentLanguage];
                speecCountOut = 5;
                npcTalkFirst = true;
                break;

            case 5:
                easyText[4] = dialog[10 * totalLanguage + currentLanguage];
                easyText[3] = dialog[11 * totalLanguage + currentLanguage];  //(Bahadýr þaþkýnlýk ve hiddetle tekrardan çenesini oynatýr ama genede ses çýkmaz.)
                speecCountOut = 6;
                break;

            case 6:
                if (PlayerPrefs.GetString("level3plyrkatlettimi") == "evet" && (PlayerPrefs.GetString("MageIsDead") == "yes" | PlayerPrefs.GetString("level6Baristilarmi") == "evet"))
                {
                    //tutarsýz
                    easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                    speecCountOut = 7;
                }
                else if (PlayerPrefs.GetString("level3plyrkatlettimi") == "evet")
                {
                    //kotu
                    easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[14 * totalLanguage + currentLanguage];
                    speecCountOut = 10;
                }
                else if (PlayerPrefs.GetString("MageIsDead") == "yes" | PlayerPrefs.GetString("level6Baristilarmi") == "evet" | PlayerPrefs.GetString("level3Secim") == "yardimEtti" | PlayerPrefs.GetString("level3Secim") == "Savaþýn")
                {
                    easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[15 * totalLanguage + currentLanguage];
                    speecCountOut = 8;
                }             
                else//hic birsey yapmamýs.
                {
                    easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[16 * totalLanguage + currentLanguage];
                    speecCountOut = 12;
                }
                
                npcTalkFirst = true;
                break;

            case 7:
                easyText[3] = "";
                easyText[4] = dialog[17 * totalLanguage + currentLanguage];
                speecCountOut = 15;
                npcTalkFirst = true;
                break;

            case 8:
                easyText[3] = "";
                easyText[4] = dialog[18 * totalLanguage + currentLanguage];      //... Aferin. Eðer benim dünyamda yaþýyor olsaydýn seni sað kolum yapardým.
                speecCountOut = 9;
                npcTalkFirst = true;
                break;

            case 9:
                easyText[3] = "";
                easyText[4] = dialog[19 * totalLanguage + currentLanguage];
                speecCountOut = 15;
                npcTalkFirst = true;
                break;

            case 10:
                easyText[3] = "";
                easyText[4] = dialog[20 * totalLanguage + currentLanguage];
                speecCountOut = 11;
                npcTalkFirst = true;
                break;

            case 11:
                easyText[3] = "";
                easyText[4] = dialog[21 * totalLanguage + currentLanguage];
                npcTalkFirst = true;
                break;

            case 12:
                easyText[3] = "";
                easyText[4] = dialog[22 * totalLanguage + currentLanguage];
                speecCountOut = 15;
                npcTalkFirst = true;
                break;

            case 15:
                easyText[3] = dialog[23 * totalLanguage + currentLanguage];
                easyText[4] = dialog[24 * totalLanguage + currentLanguage];
                speecCountOut = 16;
                npcTalkFirst = false;
                break;

            case 16:
                easyText[3] = "";
                easyText[4] = dialog[25 * totalLanguage + currentLanguage];   //Ýyi hatýrlattýn, beni dikkatle dinlemenin ödülü olarak artýk konuþabilirsin. (tekrar sýrýtýr)
                speecCountOut = 17;
                npcTalkFirst = false;
                break;

            case 17:
                easyText[3] = dialog[26 * totalLanguage + currentLanguage];
                easyText[4] = dialog[27 * totalLanguage + currentLanguage];
                speecCountOut = 18;
                npcTalkFirst = false;
                break;

            case 18:
                easyText[3] = "";
                easyText[4] = "";
                speecCountOut = 19;
                npcTalkFirst = false;
                zerlik.GetComponent<Transform>().DOMove(new Vector3(zerlik.transform.position.x, 20), 0);
                audioManager.playSound("disappear");
                StartCoroutine(BahadirSelfTalk());
                break;

            case 19:
                GetComponent<TalkWithNPC9_Zerlik>().FinishedTalk();
                break;
        }
    }

    IEnumerator BahadirSelfTalk()
    {
        yield return new WaitForSeconds(7f);
        playerTMP.text = dialog[28 * totalLanguage + currentLanguage];
        playerTMP.GetComponent<TextMeshPro>().DOFade(1, 1f);
        playerTMP.GetComponent<TextMeshPro>().DOFade(0, 0.5f).SetDelay(6f);

        yield return new WaitForSeconds(9f);
        playerTMP.text = dialog[29 * totalLanguage + currentLanguage];       //Eeee ben nasýl çýkacam þimdi burdan... Oyunlarda hiç de böyle olmuyordu.
        playerTMP.GetComponent<TextMeshPro>().DOFade(1, 1f);
        playerTMP.GetComponent<TextMeshPro>().DOFade(0, 0.5f).SetDelay(6f);

        yield return new WaitForSeconds(9f);
        playerTMP.text = dialog[30 * totalLanguage + currentLanguage];
        playerTMP.GetComponent<TextMeshPro>().DOFade(1, 1f);
        playerTMP.GetComponent<TextMeshPro>().DOFade(0, 0.5f).SetDelay(5f);

        yield return new WaitForSeconds(7f);
        audioManager.playSound("appear");
        kurgen.GetComponent<Transform>().DOMove(new Vector3(kurgen.transform.position.x, 3.47f), 0);
    }
}