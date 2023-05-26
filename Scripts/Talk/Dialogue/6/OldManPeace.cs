using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class OldManPeace : MonoBehaviour
{
    public TextMeshPro oldMan;
    public TextMeshPro[] peopleText;        //0,1 saðköy    2,3 solköy  
    public float[] fadeUpTime;
    public BoxCollider2D colliderr;
    public TextMeshPro playerText;
    AudioManager audioManager;

    int speecCount;
    bool IsFinishedTalk = true;
    bool passTheTalk;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    void Start()
    {
        passTheTalk = false;
        IsFinishedTalk = false;
        speecCount = 0;
        PlayerController.isTalking = true;
        StartCoroutine(CanWePassTalk());
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        oldMan.GetComponent<TextMeshPro>().DOFade(0, 0.5f);
        oldMan.GetComponent<TextMeshPro>().text = null;
        for (int i = 0; i < peopleText.Length; i++)
        {
            fadeUpTime[i] = 1;
            peopleText[i].GetComponent<TextMeshPro>().DOFade(0, 0.5f);
            peopleText[i].GetComponent<TextMeshPro>().text = null;
        }

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);    //dialog[0 * totalLanguage + currentLanguage];
    }

    public void TalkGrandpa()
    {
        if (!IsFinishedTalk && passTheTalk)
        {
            passTheTalk = false;
            StartCoroutine(CanWePassTalk());

            oldMan.GetComponent<TextMeshPro>().DOFade(0, 0f);
            oldMan.GetComponent<TextMeshPro>().text = null;
            for (int i = 0; i < peopleText.Length; i++)
            {
                fadeUpTime[i] = 1;
                peopleText[i].GetComponent<TextMeshPro>().DOFade(0, 0f);
                peopleText[i].GetComponent<TextMeshPro>().text = null;
            }
            audioManager.playSound("talkBtn");

            switch (speecCount)
            {
                case 0:
                    oldMan.GetComponent<TextMeshPro>().text = dialog[0 * totalLanguage + currentLanguage];
                    peopleText[0].GetComponent<TextMeshPro>().text = dialog[1 * totalLanguage + currentLanguage];
                    peopleText[1].GetComponent<TextMeshPro>().text = dialog[2 * totalLanguage + currentLanguage];
                    peopleText[2].GetComponent<TextMeshPro>().text = dialog[3 * totalLanguage + currentLanguage];
                    peopleText[3].GetComponent<TextMeshPro>().text = dialog[4 * totalLanguage + currentLanguage];

                    fadeUpTime[0] = 1f;
                    fadeUpTime[1] = 2.5f;
                    fadeUpTime[2] = 4f;
                    fadeUpTime[3] = 5.5f;
                    speecCount = 1;
                    break;

                case 1:
                    peopleText[1].GetComponent<TextMeshPro>().text = null;
                    peopleText[3].GetComponent<TextMeshPro>().text = null;
                    peopleText[0].GetComponent<TextMeshPro>().text = dialog[5 * totalLanguage + currentLanguage];
                    peopleText[2].GetComponent<TextMeshPro>().text = dialog[6 * totalLanguage + currentLanguage];

                    fadeUpTime[0] = 1f;
                    fadeUpTime[2] = 2.5f;
                    speecCount = 2;
                    break;

                case 2:
                    if (PlayerPrefs.GetString("level3plyrkatlettimi") == "evet")
                    {
                        peopleText[0].GetComponent<TextMeshPro>().text = dialog[7 * totalLanguage + currentLanguage];
                        peopleText[1].GetComponent<TextMeshPro>().text = dialog[8 * totalLanguage + currentLanguage];
                        peopleText[2].GetComponent<TextMeshPro>().text = dialog[9 * totalLanguage + currentLanguage];
                        peopleText[3].GetComponent<TextMeshPro>().text = dialog[10 * totalLanguage + currentLanguage];

                        fadeUpTime[0] = 1f;
                        fadeUpTime[1] = 4f;
                        fadeUpTime[2] = 2.5f;
                        fadeUpTime[3] = 5.5f;
                        speecCount = 3;
                    }       //3. bölümdeki köylüleri oldurunce olacak olanlar.
                    else if (PlayerPrefs.GetString("level3Secim") == "yardimEtti" && PlayerPrefs.GetString("MageIsDead") == "yes")
                    {
                        peopleText[0].GetComponent<TextMeshPro>().text = dialog[11 * totalLanguage + currentLanguage];
                        peopleText[1].GetComponent<TextMeshPro>().text = dialog[12 * totalLanguage + currentLanguage];
                        peopleText[2].GetComponent<TextMeshPro>().text = null;
                        peopleText[3].GetComponent<TextMeshPro>().text = dialog[13 * totalLanguage + currentLanguage];

                        fadeUpTime[0] = 1f;
                        fadeUpTime[1] = 2.5f;
                        fadeUpTime[2] = 4f;
                        fadeUpTime[3] = 5.5f;
                        speecCount = 7;
                    }
                    else if (PlayerPrefs.GetString("level3Secim") == "yardimEtti" | PlayerPrefs.GetString("level3Secim") == "Savaþýn")
                    {
                        peopleText[0].GetComponent<TextMeshPro>().text = dialog[14 * totalLanguage + currentLanguage];
                        peopleText[1].GetComponent<TextMeshPro>().text = dialog[15 * totalLanguage + currentLanguage];
                        peopleText[2].GetComponent<TextMeshPro>().text = null;
                        peopleText[3].GetComponent<TextMeshPro>().text = dialog[16 * totalLanguage + currentLanguage];

                        fadeUpTime[0] = 4f;
                        fadeUpTime[1] = 2.5f;
                        fadeUpTime[2] = 4f;
                        fadeUpTime[3] = 1f;
                        speecCount = 7;
                    }
                    else if (PlayerPrefs.GetString("MageIsDead") == "yes")
                    {
                        peopleText[0].GetComponent<TextMeshPro>().text = dialog[17 * totalLanguage + currentLanguage];
                        peopleText[1].GetComponent<TextMeshPro>().text = dialog[18 * totalLanguage + currentLanguage];
                        peopleText[2].GetComponent<TextMeshPro>().text = null;
                        peopleText[3].GetComponent<TextMeshPro>().text = dialog[19 * totalLanguage + currentLanguage];

                        fadeUpTime[0] = 4f;
                        fadeUpTime[1] = 2.5f;
                        fadeUpTime[2] = 4f;
                        fadeUpTime[3] = 1f;
                        speecCount = 7;
                    }
                    else
                    {
                        peopleText[0].GetComponent<TextMeshPro>().text = dialog[20 * totalLanguage + currentLanguage];
                        peopleText[1].GetComponent<TextMeshPro>().text = dialog[21 * totalLanguage + currentLanguage];
                        peopleText[2].GetComponent<TextMeshPro>().text = dialog[22 * totalLanguage + currentLanguage];
                        peopleText[3].GetComponent<TextMeshPro>().text = null;

                        fadeUpTime[0] = 1f;
                        fadeUpTime[1] = 2.5f;
                        fadeUpTime[2] = 4f;
                        fadeUpTime[3] = 5.5f;
                        speecCount = 3;
                    }
                    break;

                case 3:
                    if (PlayerPrefs.GetString("level3plyrkatlettimi") == "evet")
                        peopleText[0].GetComponent<TextMeshPro>().text = dialog[23 * totalLanguage + currentLanguage];
                    else
                        peopleText[0].GetComponent<TextMeshPro>().text = dialog[24 * totalLanguage + currentLanguage];

                    peopleText[1].GetComponent<TextMeshPro>().text = dialog[25 * totalLanguage + currentLanguage];
                    peopleText[2].GetComponent<TextMeshPro>().text = dialog[26 * totalLanguage + currentLanguage];
                    peopleText[3].GetComponent<TextMeshPro>().text = dialog[27 * totalLanguage + currentLanguage];

                    fadeUpTime[0] = 5.5f;
                    fadeUpTime[1] = 2.5f;
                    fadeUpTime[2] = 4f;
                    fadeUpTime[3] = 1f;
                    speecCount = 4;
                    break;

                case 4:
                    if (PlayerPrefs.GetString("level3plyrkatlettimi") == "evet")
                        oldMan.GetComponent<TextMeshPro>().text = dialog[28 * totalLanguage + currentLanguage];
                    else
                        oldMan.GetComponent<TextMeshPro>().text = dialog[29 * totalLanguage + currentLanguage];

                    speecCount = 5;
                    break;

                case 5:
                    PlayerPrefs.SetString("L6VillagesInPeace", "denedi");
                    PlayerController.isTalking = false;
                    playerText.text = dialog[38 * totalLanguage + currentLanguage];
                    playerText.DOFade(1, 0.5f);
                    playerText.DOFade(0, 0.5f).SetDelay(6f);
                    break;

                case 6:
                    oldMan.GetComponent<TextMeshPro>().text = dialog[30 * totalLanguage + currentLanguage];
                    peopleText[0].GetComponent<TextMeshPro>().text = dialog[31 * totalLanguage + currentLanguage];
                    peopleText[1].GetComponent<TextMeshPro>().text = dialog[32 * totalLanguage + currentLanguage];
                    peopleText[3].GetComponent<TextMeshPro>().text = dialog[33 * totalLanguage + currentLanguage];

                    fadeUpTime[0] = 4f;
                    fadeUpTime[1] = 2.5f;
                    fadeUpTime[2] = 4f;
                    fadeUpTime[3] = 1f;
                    speecCount = 7;
                    break;

                case 7:
                    oldMan.GetComponent<TextMeshPro>().text = dialog[34 * totalLanguage + currentLanguage];
                    peopleText[0].GetComponent<TextMeshPro>().text = dialog[35 * totalLanguage + currentLanguage];
                    peopleText[1].GetComponent<TextMeshPro>().text = dialog[36 * totalLanguage + currentLanguage];
                    peopleText[3].GetComponent<TextMeshPro>().text = dialog[37 * totalLanguage + currentLanguage];

                    fadeUpTime[0] = 4f;
                    fadeUpTime[1] = 2.5f;
                    fadeUpTime[2] = 4f;
                    fadeUpTime[3] = 1f;
                    speecCount = 8;
                    break;

                case 8:
                    PlayerPrefs.SetString("L6VillagesInPeace", "evet");
                    colliderr.enabled = true;
                    PlayerController.isTalking = false;
                    break;
            }

            if (oldMan.GetComponent<TextMeshPro>().text != null)
                oldMan.GetComponent<TextMeshPro>().DOFade(1, 0.7f);

            for (int i = 0; i < peopleText.Length; i++)
            {
                if (peopleText[i].GetComponent<TextMeshPro>().text != null)
                {
                    peopleText[i].GetComponent<TextMeshPro>().DOFade(1, 0.7f).SetDelay(fadeUpTime[i]);
                }
            }
        }
    }

    IEnumerator CanWePassTalk()
    {
        yield return new WaitForSeconds(1.5f);
        passTheTalk = true;
    }
}
