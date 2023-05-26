using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TalkWithNPC6_BadGuy : MonoBehaviour
{
    public GameObject NPCTalk;                                      // farklý npc ler ile konususabiliriz diye bunu bizim atamamaýz lazým
    [HideInInspector] public GameObject electionPanel;
    [HideInInspector] public Transform[] BText = new Transform[3];
    [HideInInspector] public GameObject eB1, eB2, eB3;
    [HideInInspector] public GameObject playerTalk;
    [HideInInspector] public GameObject player;
    [HideInInspector] public bool canIPassTalk;
    public bool isNPCTalkFirst;

    public static bool isFinishedTalk;
    int speecCount;
    AudioManager audioManager;


    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        eB1 = GameObject.Find("B1");
        eB2 = GameObject.Find("B2");
        eB3 = GameObject.Find("B3");
        player = GameObject.Find("Player");
        playerTalk = GameObject.Find("PlayerTalk");
        electionPanel = GameObject.Find("ElectionPanel");

        BText[0] = eB1.transform.GetChild(0);
        BText[1] = eB2.transform.GetChild(0);
        BText[2] = eB3.transform.GetChild(0);

        canIPassTalk = true;
        isFinishedTalk = false;
        speecCount = 0;

        PlayerPrefs.SetString("level6Baristilarmi", "hayýr");//kaldýrýlacak

        FinishedTalk();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (false/*!lookNPCface*/)
            {
                //FinishedTalk();
            }
            else if (!isFinishedTalk && !PlayerController.isTalking && PlayerPrefs.GetString("level6Baristilarmi") == "evet") //ppkýsmý 6. bolumden sonra kaldýrýlacak 
            {
                PlayerController.WhatIsNameToTalkIam = transform.name;
                StartTalk();

                if (isNPCTalkFirst)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().GoLeft(false);//cýkarýlacak
                    GameObject.Find("Player").GetComponent<PlayerController>().GoRight(false); //cýkarýlacak
                    PlayerController.isTalking = true;
                    ForwardSpeech();
                }
            }
        }      
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") & !isFinishedTalk)
        {
            FinishedTalk();
        }
    }   

    public void StartTalk()                                           //Konusma basladýgýnda olacaklar
    {
        electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);                                   
        electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.5f);
        NPCTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);
        playerTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);

        if(!isNPCTalkFirst)
        {
            if (speecCount == 0)
                GetComponent<BadGuyDialog6>().FirsWords();
            BText[0].GetComponent<TextMeshProUGUI>().text = GetComponent<BadGuyDialog6>().easyText[0];
            BText[1].GetComponent<TextMeshProUGUI>().text = GetComponent<BadGuyDialog6>().easyText[1];
            BText[2].GetComponent<TextMeshProUGUI>().text = GetComponent<BadGuyDialog6>().easyText[2];
        }
        IsTextNullBtnCantInteractible();
    }
    public void FinishedTalk()                                        //konusma bittiginde konusma ekraný ayarlarýný sýfýrlar
    {
        //if (PlayerController.isTalking)//cýkarýlacak
            //GetComponent<BoxCollider2D>().enabled = false;//cýkarýlacak

        PlayerController.isTalking = false;
        electionPanel.GetComponent<CanvasGroup>().DOFade(0, 0f);
        electionPanel.GetComponent<RectTransform>().DOScale(0, 0f);

        NPCTalk.GetComponent<TextMeshPro>().text = null;
        playerTalk.GetComponent<TextMeshPro>().text = null;

        int aa = BText.Length;
        for (int i = 0; i < aa; i++)
             BText[i].GetComponent<TextMeshProUGUI>().text = null;

        if (speecCount > 99)
        {
            speecCount = 100;

            if (PlayerController.isTalking)//cýkarýlacak
                GetComponent<BoxCollider2D>().enabled = false;//cýkarýlacak
        }
        else if (speecCount > 74)
        {
            speecCount = 75;
        }
        else if (speecCount > 49) 
        {
            speecCount = 50;
        }
        else if (speecCount > 24)
        {
            speecCount = 25;
        }
        else
        {
            speecCount = 0;
        }
        GetComponent<BadGuyDialog6>().speecCountOut = speecCount;
    }
    public void FinishedTalkJumpBtn()
    {
        if (PlayerController.isTalking)
            FinishedTalk();
    }

    public void EB(int ButonNo)
    {
        if (PlayerController.WhatIsNameToTalkIam == transform.name)//bu olmazsa bütün konusma scriptleri ayný anda calýsýyor
        {
            PlayerController.isTalking = true;
            speecCount = GetComponent<BadGuyDialog6>().speecCountOut;
            GetComponent<BadGuyDialog6>().EB(ButonNo, speecCount);
            canIPassTalk = false;

            NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<BadGuyDialog6>().easyText[4];
            NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.6f);
            if (GetComponent<BadGuyDialog6>().easyText[3] == null)
                playerTalk.GetComponent<TextMeshPro>().text = BText[ButonNo - 1].GetComponent<TextMeshProUGUI>().text;
            else if (GetComponent<BadGuyDialog6>().easyText[3] == "null")
                playerTalk.GetComponent<TextMeshPro>().text = null;
            else
                playerTalk.GetComponent<TextMeshPro>().text = GetComponent<BadGuyDialog6>().easyText[3];
            playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0f);

            electionPanel.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
            electionPanel.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.1f);

            IsTextNullBtnCantInteractible();
            StartCoroutine(PassTalk(1f));
        }
    }
    public void ForwardSpeech()
    {
        if (canIPassTalk)
        {
            speecCount = GetComponent<BadGuyDialog6>().speecCountOut;
            GetComponent<BadGuyDialog6>().ForwardSpeech(speecCount);
            NPCTalk.GetComponent<TextMeshPro>().text = null;
            playerTalk.GetComponent<TextMeshPro>().text = null;
            NPCTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);
            playerTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);

            int aa = BText.Length;
            for (int i = 0; i < aa; i++) 
                BText[i].GetComponent<TextMeshProUGUI>().text = null;

            if (speecCount == 100)
            {
                isFinishedTalk = true;
                FinishedTalk();
            }
            else if (speecCount == 25)
            {
                FinishedTalk();
            }
            else if (speecCount == 50)
            {
                FinishedTalk();
            }

            //player ýn  konusmasý boþ ise ve konuþuyorsam
            if (GetComponent<BadGuyDialog6>().easyText[3] == null & PlayerController.isTalking)
            {
                electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);
                electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.15f);
                for (int i = 0; i < BText.Length; i++)
                    BText[i].GetComponent<TextMeshProUGUI>().text = GetComponent<BadGuyDialog6>().easyText[i];
            }
            else if (!GetComponent<BadGuyDialog6>().npcTalkFirst)
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);

                speecCount = GetComponent<BadGuyDialog6>().speecCountOut;
            }
            else
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);

                speecCount = GetComponent<BadGuyDialog6>().speecCountOut;
            }
            IsTextNullBtnCantInteractible();
        }
    }

    public void IsTextNullBtnCantInteractible()
    {
        float a;
        if (BText[0].GetComponent<TextMeshProUGUI>().text == null)
        {
            eB1.GetComponent<Button>().interactable = false;
            eB1.GetComponent<Image>().enabled = false;
        }
        else
        {
            eB1.GetComponent<Button>().interactable = true;
            eB1.GetComponent<Image>().enabled = true;

            a = BText[0].GetComponent<TextMeshProUGUI>().preferredWidth;
            eB1.GetComponent<RectTransform>().sizeDelta = new Vector2(a + 90, eB1.GetComponent<RectTransform>().sizeDelta.y);
        }

        if (BText[1].GetComponent<TextMeshProUGUI>().text == null)
        {
            eB2.GetComponent<Button>().interactable = false;
            eB2.GetComponent<Image>().enabled = false;
        }
        else
        {
            eB2.GetComponent<Button>().interactable = true;
            eB2.GetComponent<Image>().enabled = true;

            a = BText[1].GetComponent<TextMeshProUGUI>().preferredWidth;
            eB2.GetComponent<RectTransform>().sizeDelta = new Vector2(a + 90, eB1.GetComponent<RectTransform>().sizeDelta.y);
        }

        if (BText[2].GetComponent<TextMeshProUGUI>().text == null)
        {
            eB3.GetComponent<Button>().interactable = false;
            eB3.GetComponent<Image>().enabled = false;
        }
        else
        {
            eB3.GetComponent<Button>().interactable = true;
            eB3.GetComponent<Image>().enabled = true;

            a = BText[2].GetComponent<TextMeshProUGUI>().preferredWidth;
            eB3.GetComponent<RectTransform>().sizeDelta = new Vector2(a + 90, eB1.GetComponent<RectTransform>().sizeDelta.y);
        }
    }
    public void ForwardSpeechFromScreen()
    {
        if (PlayerController.isTalking && canIPassTalk && PlayerController.WhatIsNameToTalkIam == transform.name)
        {
            audioManager.playSound("passTalk");
            ForwardSpeech();
            canIPassTalk = false;
            StartCoroutine(PassTalk(1f));
            if (GetComponent<BadGuyDialog6>().threeQuestion >= 3)//silinecek
            {
                playerTalk.GetComponent<TextMeshPro>().text = GetComponent<BadGuyDialog6>().stringForOut;
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 1);
            }
        }
    }
    IEnumerator PassTalk(float WaithSeconds)
    {
        yield return new WaitForSeconds(WaithSeconds);
        canIPassTalk = true;
    }
    IEnumerator NPCAndPlayerWords(float WaithSeconds)
    {
        yield return new WaitForSeconds(WaithSeconds);
        playerTalk.GetComponent<TextMeshPro>().text = GetComponent<BadGuyDialog6>().easyText[3];
        NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<BadGuyDialog6>().easyText[4];
    }
}
