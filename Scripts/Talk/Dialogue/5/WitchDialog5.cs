using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class WitchDialog5 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz
    AudioManager audioManager;

    //silinecekler buranýn altýnda yazýlacak
    [HideInInspector] public int threeQuestion;                                   //3 soruyuda sordugu zaman hýc mantýklý deðil der bahadýr.
    public BoxCollider2D talkCollider, yaverCollider, wallColl;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector]public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;
        threeQuestion = 0;

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);        //dialog[0 * totalLanguage + currentLanguage];
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
                        StartCoroutine(Attack());
                        break;

                    case 2:
                        easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                        speecCountOut = 3;
                        StartCoroutine(Attack());
                        break;
                }
                break;

            case 2:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[2 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
                        StartCoroutine(Attack());
                        break;

                    case 2:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 2;
                        StartCoroutine(Attack());
                        break;
                }
                break;

            case 7:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                        speecCountOut = 8;
                        break;

                    case 2:
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                        speecCountOut = 11;
                        break;
                }
                break;

            case 10:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                        //GetComponent<DropGold>().eb.ourCoin -= 15;
                        speecCountOut = 0;
                        GetComponent<TalkWithNPC5_Witch>().FinishedTalk();
                        break;

                    case 2:
                        speecCountOut = 7;
                        GetComponent<TalkWithNPC5_Witch>().ForwardSpeech();
                        break;
                }
                break;

            case 13:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = null;
                        speecCountOut = 14;
                        break;

                    case 2:
                        easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                        speecCountOut = 15;
                        break;
                }
                break;

            case 17:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = null;
                        speecCountOut = 14;
                        break;

                    case 2:
                        easyText[4] = dialog[8 * totalLanguage + currentLanguage];
                        speecCountOut = 18;
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

        if (PlayerPrefs.GetString("ÝsTalkWoman") == "yes")  //öldürmeyi kabul etti
        {
            speecCount = 5;
            PlayerPrefs.SetString("ÝsTalkWoman", "ayes");
        }

        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                easyText[3] = "";
                easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[0] = dialog[10 * totalLanguage + currentLanguage];
                easyText[1] = dialog[11 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 2:
                easyText[3] = "";
                easyText[4] = null;
                GetComponent<TalkWithNPC5_Witch>().FinishedTalk();
                TalkWithNPC5_Witch.isFinishedTalk = true;
                break;

            case 3:
                easyText[3] = "";
                easyText[4] = null;
                GetComponent<TalkWithNPC5_Witch>().FinishedTalk();
                break;

            case 5:
                easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                speecCountOut = 6;
                npcTalkFirst = true;
                break;

            case 6:
                easyText[3] = "";
                easyText[4] = dialog[14 * totalLanguage + currentLanguage];
                speecCountOut = 7;
                npcTalkFirst = true;
                break;

            case 7:
                easyText[0] = dialog[15 * totalLanguage + currentLanguage];
                easyText[1] = dialog[16 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 8:
                easyText[3] = dialog[17 * totalLanguage + currentLanguage];
                easyText[4] = dialog[18 * totalLanguage + currentLanguage];
                speecCountOut = 10;
                npcTalkFirst = false;
                break;

            case 10:
                easyText[0] = dialog[19 * totalLanguage + currentLanguage];
                easyText[1] = dialog[20 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 11:
                easyText[3] = dialog[21 * totalLanguage + currentLanguage];
                easyText[4] = dialog[22 * totalLanguage + currentLanguage];
                speecCountOut = 12;
                npcTalkFirst = false;
                break;

            case 12:
                easyText[3] = dialog[23 * totalLanguage + currentLanguage];
                easyText[4] = dialog[24 * totalLanguage + currentLanguage];
                speecCountOut = 13;
                npcTalkFirst = false;
                break;

            case 13:
                easyText[0] = dialog[25 * totalLanguage + currentLanguage];
                easyText[1] = dialog[26 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 14:
                GetComponent<TalkWithNPC5_Witch>().FinishedTalk();
                speecCountOut = 100;
                StartCoroutine(Attack());
                break;

            case 15:
                easyText[3] = dialog[27 * totalLanguage + currentLanguage];
                easyText[4] = dialog[28 * totalLanguage + currentLanguage];
                speecCountOut = 16;
                npcTalkFirst = false;
                break;

            case 16:
                easyText[3] = dialog[29 * totalLanguage + currentLanguage];
                easyText[4] = dialog[30 * totalLanguage + currentLanguage];
                speecCountOut = 17;
                npcTalkFirst = false;
                break;

            case 17:
                easyText[0] = dialog[31 * totalLanguage + currentLanguage];
                easyText[1] = dialog[32 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 18:
                easyText[3] = dialog[33 * totalLanguage + currentLanguage];
                easyText[4] = dialog[34 * totalLanguage + currentLanguage];
                speecCountOut = 14;
                npcTalkFirst = false;
                break;
        }
    }

    public void ForTWNPC5()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<TalkWithNPC5_Witch>().NPCTalk.GetComponent<TextMeshPro>().text = "Ýyi madem.";
        GetComponent<TalkWithNPC5_Witch>().NPCTalk.GetComponent<TextMeshPro>().DOComplete();
        GetComponent<TalkWithNPC5_Witch>().NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.5f);
        talkCollider.enabled = false;
        yaverCollider.enabled = false;
        wallColl.enabled = false;

        yield return new WaitForSeconds(1f);
        GetComponent<TalkWithNPC5_Witch>().NPCTalk.GetComponent<TextMeshPro>().DOFade(0, 0.5f).SetDelay(1.5f);
        GetComponent<EnemyRangedAII>().activeDistance = 14;
        GetComponent<EnemyRangedAII>().attackDistance = 10;
        yield return new WaitForSeconds(1f);
        GetComponent<TalkWithNPC5_Witch>().NPCTalk.GetComponent<TextMeshPro>().text = null;
    }
}