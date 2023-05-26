using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TalkWithNPC : MonoBehaviour
{
    public GameObject electionPanel;
    public GameObject NPCTalk;
    public GameObject[] BText;
    public GameObject eB1, eB2, eB3;
    public GameObject playerTalk;
    public GameObject player;
    public GameObject otherPlayerTalk;
    public string[] education;

    public static bool isFinishedTalk;
    int speecCount;
    bool canIPassTalk, forStay;
    [HideInInspector]public bool firstTime;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        canIPassTalk = true;
        firstTime = true;
        forStay = true;
        isFinishedTalk = false;
        speecCount = 0;

        FinishedTalk();
    }

    private void OnTriggerStay2D(Collider2D other)
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

        if (firstTime && forStay)
        {
            forStay = false;
            otherPlayerTalk.GetComponent<TextMeshPro>().text = education[PlayerPrefs.GetInt("language")];
            otherPlayerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.5f);
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

    public void Bir()
    {
        otherPlayerTalk.GetComponent<TextMeshPro>().text = null;
        otherPlayerTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);
    }
    public void StartTalk()                                           //Konusma basladýgýnda olacaklar
    {
        electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);                                       
        electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.5f);
        NPCTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);
        playerTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);

        IsTextNullBtnCantInteractible();
        if(speecCount==0)
            GetComponent<_1ZerlikTalk>().FirsWords();
        BText[0].GetComponent<TextMeshProUGUI>().text = GetComponent<_1ZerlikTalk>().easyText[0]; 
        BText[1].GetComponent<TextMeshProUGUI>().text = GetComponent<_1ZerlikTalk>().easyText[1];
        BText[2].GetComponent<TextMeshProUGUI>().text = GetComponent<_1ZerlikTalk>().easyText[2];
    }
    public void FinishedTalk()                                        //konusma bittiginde konusma ekraný ayarlarýný sýfýrlar
    {
        otherPlayerTalk.GetComponent<TextMeshPro>().text = null;
        otherPlayerTalk.GetComponent<TextMeshPro>().DOFade(0, 0f);
        forStay = true;
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
            GetComponent<_1ZerlikTalk>().EB(ButonNo, speecCount);
            PlayerController.isTalking = true;
            canIPassTalk = false;

            NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<_1ZerlikTalk>().easyText[4];
            NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.6f);
            playerTalk.GetComponent<TextMeshPro>().text = BText[ButonNo - 1].GetComponent<TextMeshProUGUI>().text;
            playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0f);

            electionPanel.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
            electionPanel.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.1f);

            speecCount = GetComponent<_1ZerlikTalk>().speecCountOut;
            IsTextNullBtnCantInteractible();
            StartCoroutine(PassTalk(1f));
    }
    public void ForwardSpeech()
    {
        if (canIPassTalk)
        {
            PlayerController.isTalking = true;
            GetComponent<_1ZerlikTalk>().ForwardSpeech(speecCount);
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
            if (GetComponent<_1ZerlikTalk>().easyText[3] == null & PlayerController.isTalking)
            {
                electionPanel.GetComponent<RectTransform>().DOScale(1, 0f);
                electionPanel.GetComponent<CanvasGroup>().DOFade(0.75f, 0.15f);
                for (int i = 0; i < BText.Length; i++)
                    BText[i].GetComponent<TextMeshProUGUI>().text = GetComponent<_1ZerlikTalk>().easyText[i];
            }
            else if (!GetComponent<_1ZerlikTalk>().npcTalkFirst)
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);

                speecCount = GetComponent<_1ZerlikTalk>().speecCountOut;
            }
            else
            {
                StartCoroutine(NPCAndPlayerWords(0.1f));
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.55f);
                NPCTalk.GetComponent<TextMeshPro>().DOFade(1, 0.3f).SetDelay(0.1f);

                speecCount = GetComponent<_1ZerlikTalk>().speecCountOut;
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
        playerTalk.GetComponent<TextMeshPro>().text = GetComponent<_1ZerlikTalk>().easyText[3];
        NPCTalk.GetComponent<TextMeshPro>().text = GetComponent<_1ZerlikTalk>().easyText[4];
    }
}
