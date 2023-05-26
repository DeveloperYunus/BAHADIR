using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TalkSystem3Zerlik : MonoBehaviour
{
    public GameObject NPCTalk;                                      // farklý npc ler ile konususabiliriz diye bunu bizim atamamaýz lazým
    [HideInInspector] public GameObject electionPanel;
    [HideInInspector] public Transform[] BText = new Transform[3];
    [HideInInspector] public GameObject eB1, eB2, eB3;
    [HideInInspector] public GameObject playerTalk;
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject stopTalkBtn;
    [HideInInspector] public bool canIPassTalk;
    public bool NPCTalkFirst, isPlayerStopTalk;

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
        stopTalkBtn = GameObject.Find("JumpBtn");

        BText[0] = eB1.transform.GetChild(0);
        BText[1] = eB2.transform.GetChild(0);
        BText[2] = eB3.transform.GetChild(0);

        canIPassTalk = true;
        isFinishedTalk = false;
        speecCount = 0;

        FinishedTalk();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool lookNPCface;
        float fark = player.transform.position.x - transform.position.x;

        //fark - ise NPC nin solundayým demektýr. bu durumda konusmak icin scale.x - olmalý
        if (fark < 0)
        {
            if (player.transform.localScale.x < 0) lookNPCface = true;
            else lookNPCface = false;
        }
        else
        {
            if (player.transform.localScale.x < 0) lookNPCface = false;
            else lookNPCface = true;
        }

        if (other.CompareTag("Player"))
        {
            if (!lookNPCface)
            {
                FinishedTalk();
            }
            else if (!isFinishedTalk && !PlayerController.isTalking) 
            {
                StartTalk();

                if (NPCTalkFirst)
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
        stopTalkBtn.GetComponent<Button>().interactable = false;
        electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);                                       
        electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.5f);
        NPCTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);
        playerTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);

        IsTextNullBtnCantInteractible();
        if(NPCTalkFirst)
        {
            if (speecCount == 0)
                GetComponent<ZerlikTalk3>().FirsWords();
            BText[0].GetComponent<TextMeshProUGUI>().text = GetComponent<ZerlikTalk3>().easyText[0];
            BText[1].GetComponent<TextMeshProUGUI>().text = GetComponent<ZerlikTalk3>().easyText[1];
            BText[2].GetComponent<TextMeshProUGUI>().text = GetComponent<ZerlikTalk3>().easyText[2];
        }
    }
    public void FinishedTalk()                                        //konusma bittiginde konusma ekraný ayarlarýný sýfýrlar
    {
        if (PlayerController.isTalking)
            GetComponent<BoxCollider2D>().enabled = false;

        PlayerController.isTalking = false;
        stopTalkBtn.GetComponent<Button>().interactable = true;
        electionPanel.GetComponent<CanvasGroup>().DOFade(0, 0f);
        electionPanel.GetComponent<RectTransform>().DOScale(0, 0f);

        NPCTalk.GetComponent<TextMeshPro>().text = null;
        playerTalk.GetComponent<TextMeshPro>().text = null;

        for (int i = 0; i < BText.Length; i++)
             BText[i].GetComponent<TextMeshProUGUI>().text = null;

        if (speecCount > 99)
        {
            speecCount = 100;
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
    }

    public void EB(int ButonNo)
    {        
        PlayerController.isTalking = true;
        speecCount = GetComponent<ZerlikTalk3>().speecCountOut;
        GetComponent<ZerlikTalk3>().EB(ButonNo, speecCount);
        canIPassTalk = false;

        NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<ZerlikTalk3>().easyText[4];
        NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.6f);
        if (GetComponent<ZerlikTalk3>().easyText[3] == null) 
            playerTalk.GetComponent<TextMeshPro>().text = BText[ButonNo - 1].GetComponent<TextMeshProUGUI>().text;
        else if (GetComponent<ZerlikTalk3>().easyText[3] == "null")
            playerTalk.GetComponent<TextMeshPro>().text = null;
        else
            playerTalk.GetComponent<TextMeshPro>().text = GetComponent<ZerlikTalk3>().easyText[3];
        playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0f);
        
        electionPanel.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        electionPanel.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.1f);

        IsTextNullBtnCantInteractible();
        StartCoroutine(PassTalk(1f));        
    }
    public void ForwardSpeech()
    {
        if (canIPassTalk)
        {
            speecCount = GetComponent<ZerlikTalk3>().speecCountOut;
            GetComponent<ZerlikTalk3>().ForwardSpeech(speecCount);
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
            if (GetComponent<ZerlikTalk3>().easyText[3] == null & PlayerController.isTalking)
            {
                electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);
                electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.15f);
                for (int i = 0; i < BText.Length; i++)
                    BText[i].GetComponent<TextMeshProUGUI>().text = GetComponent<ZerlikTalk3>().easyText[i];
            }
            else if (!GetComponent<ZerlikTalk3>().npcTalkFirst)
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);

                speecCount = GetComponent<ZerlikTalk3>().speecCountOut;
            }
            else
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);

                speecCount = GetComponent<ZerlikTalk3>().speecCountOut;
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
        if (PlayerController.isTalking && canIPassTalk && Vector2.SqrMagnitude(transform.position - player.transform.position) < 50)
        {
            audioManager.playSound("passTalk");

            ForwardSpeech();
            canIPassTalk = false;
            StartCoroutine(PassTalk(1f));
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
        playerTalk.GetComponent<TextMeshPro>().text = GetComponent<ZerlikTalk3>().easyText[3];
        NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<ZerlikTalk3>().easyText[4];
    }
}
