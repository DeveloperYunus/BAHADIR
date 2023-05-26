using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class KurgenDialog9 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz

    //silinecekler
    public GameObject kurgen;
    public TextMeshPro playerTMP;
    AudioManager audioManager;
    public Animator door, levelLoader;
    public TextMeshProUGUI L9text;

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
        switch (speecCount)
        {
            case 4:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[0 * totalLanguage + currentLanguage];
                        speecCountOut = 5;
                        break;

                    case 2:
                        easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                        speecCountOut = 10;
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
                easyText[3] = dialog[2 * totalLanguage + currentLanguage];
                easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                easyText[3] = dialog[5 * totalLanguage + currentLanguage];
                speecCountOut = 2;
                npcTalkFirst = true;
                break;

            case 2:
                easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                easyText[3] = dialog[7 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                npcTalkFirst = true;
                break;

            case 3:
                easyText[4] = dialog[8 * totalLanguage + currentLanguage];
                easyText[3] = "";
                speecCountOut = 4;
                npcTalkFirst = true;
                break;

            case 4:
                easyText[0] = dialog[9 * totalLanguage + currentLanguage];
                easyText[1] = dialog[10 * totalLanguage + currentLanguage];          //Risk almayacaðým yinede saðol.
                easyText[2] = null;
                break;

            case 5:
                easyText[4] = dialog[11 * totalLanguage + currentLanguage];
                easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                npcTalkFirst = false;
                speecCountOut = 6;
                break;

            case 6:
                easyText[4] = "";
                easyText[3] = "";
                speecCountOut = 11;
                audioManager.playSound("doorOpen");
                door.Play("L9PrisonDoorOpen");
                kurgen.GetComponent<Transform>().DOMove(new Vector3(kurgen.transform.position.x, 20), 0);
                break;


            case 10:
                easyText[3] = "";
                easyText[4] = "";
                speecCountOut = 11;
                npcTalkFirst = false;
                audioManager.playSound("disappear");
                StartCoroutine(BahadirSelfTalk());
                kurgen.GetComponent<Transform>().DOMove(new Vector3(kurgen.transform.position.x, 20), 0);
                break;

            case 11:
                GetComponent<TalkWithNPC9_Kurgen>().FinishedTalk();
                easyText[3] = "";
                easyText[4] = "";
                break;
        }
    }

    IEnumerator BahadirSelfTalk()// kurgen gittikden sonra bahadýr kendýsýyle konusur
    {
        yield return new WaitForSeconds(6f);
        playerTMP.text = dialog[13 * totalLanguage + currentLanguage];
        playerTMP.GetComponent<TextMeshPro>().DOFade(0, 0f);
        playerTMP.GetComponent<TextMeshPro>().DOFade(1, 1f);
        playerTMP.GetComponent<TextMeshPro>().DOFade(0, 0.5f).SetDelay(6f);

        yield return new WaitForSeconds(8f);
        playerTMP.text = dialog[14 * totalLanguage + currentLanguage];
        playerTMP.GetComponent<TextMeshPro>().DOFade(1, 1f);
        playerTMP.GetComponent<TextMeshPro>().DOFade(0, 0.5f).SetDelay(6f);

        yield return new WaitForSeconds(6f);
        levelLoader.Play("L9Image");
        L9text.text = dialog[15 * totalLanguage + currentLanguage];          //<size=120>SON</size>\n\nBahadýr uzuun süre Zerliði bekledi. Çünkü bu senaryolarý yazmak onun için bile bir bir hayli zordu.
    }
}