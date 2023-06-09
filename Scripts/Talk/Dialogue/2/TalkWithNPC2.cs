using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TalkWithNPC2 : MonoBehaviour
{
    public GameObject electionPanel;
    public GameObject NPCTalk;
    [HideInInspector] public Transform[] BText = new Transform[3];
    public GameObject eB1, eB2, eB3;
    public GameObject playerTalk;
    public GameObject player;
    public bool firstTalkTypeEB;

    public static bool isFinishedTalk;
    int speecCount;
    bool canIPassTalk;
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

        FinishedTalk();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool lookNPCface;
        float fark = player.transform.position.x - transform.position.x;

        //fark - ise NPC nin solunday�m demekt�r. bu durumda konusmak icin scale.x - olmal�
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

                if (!firstTalkTypeEB)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().GoLeft(false);//c�kar�lacak
                    GameObject.Find("Player").GetComponent<PlayerController>().GoRight(false); //c�kar�lacak
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

   
    public void StartTalk()                                           //Konusma baslad�g�nda olacaklar
    {
        electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);                                       
        electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.5f);
        NPCTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);
        playerTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);

        IsTextNullBtnCantInteractible();
        if(firstTalkTypeEB)
        {
            if (speecCount == 0)
                GetComponent<_2GolemTalk>().FirsWords();
            BText[0].GetComponent<TextMeshProUGUI>().text = GetComponent<_2GolemTalk>().easyText[0];
            BText[1].GetComponent<TextMeshProUGUI>().text = GetComponent<_2GolemTalk>().easyText[1];
            BText[2].GetComponent<TextMeshProUGUI>().text = GetComponent<_2GolemTalk>().easyText[2];
        }
    }
    public void FinishedTalk()                                        //konusma bittiginde konusma ekran� ayarlar�n� s�f�rlar
    {
        if (PlayerController.isTalking)//c�kar�lacak
            GetComponent<BoxCollider2D>().enabled = false;//c�kar�lacak

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
    public void FinishedTalkJumpBtn()
    {
        if (PlayerController.isTalking)
            FinishedTalk();
    }

    public void EB(int ButonNo)
    {        
        PlayerController.isTalking = true;
        speecCount = GetComponent<_2GolemTalk>().speecCountOut;
        GetComponent<_2GolemTalk>().EB(ButonNo, speecCount);
        canIPassTalk = false;

        NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<_2GolemTalk>().easyText[4];
        NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.6f);
        playerTalk.GetComponent<TextMeshPro>().text = BText[ButonNo - 1].GetComponent<TextMeshProUGUI>().text;
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
            speecCount = GetComponent<_2GolemTalk>().speecCountOut;
            GetComponent<_2GolemTalk>().ForwardSpeech(speecCount);
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

            //player �n  konusmas� bo� ise ve konu�uyorsam
            if (GetComponent<_2GolemTalk>().easyText[3] == null & PlayerController.isTalking)
            {
                electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);
                electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.15f);
                for (int i = 0; i < BText.Length; i++)
                    BText[i].GetComponent<TextMeshProUGUI>().text = GetComponent<_2GolemTalk>().easyText[i];
            }
            else if (!GetComponent<_2GolemTalk>().npcTalkFirst)
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);

                speecCount = GetComponent<_2GolemTalk>().speecCountOut;
            }
            else
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);

                speecCount = GetComponent<_2GolemTalk>().speecCountOut;
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
        if (PlayerController.isTalking & canIPassTalk)
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
        playerTalk.GetComponent<TextMeshPro>().text = GetComponent<_2GolemTalk>().easyText[3];
        NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<_2GolemTalk>().easyText[4];
    }
}
