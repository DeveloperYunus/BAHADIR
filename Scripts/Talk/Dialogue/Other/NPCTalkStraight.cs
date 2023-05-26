using UnityEngine;
using DG.Tweening;
using TMPro;

public class NPCTalkStraight : MonoBehaviour
{
    TextMeshPro NPCText, PlayerText;
    public string[] NPCTalk, PlayerTalk;
    public float showWordsTime;
    public bool isPlayerFirst;

    [Header("Need PP")]
    public bool isNeedPP;
    public string ppName, whatShouldBeString;

    bool firstTime;

    private void Start()
    {
        PlayerText = GameObject.Find("PlayerTalk").GetComponent<TextMeshPro>();
        NPCText = GetComponentInChildren<TextMeshPro>();

        firstTime = true;
        NPCText.DOFade(0,0f);
        NPCText.text = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool bol;
        
        if (!isNeedPP) bol = true;
        else
        {
            if (PlayerPrefs.GetString(ppName) == whatShouldBeString) bol = true;
            else bol = false;
        }

        if (other.CompareTag("Player") && firstTime && bol)
        {
            float a;
            PlayerText.DOComplete();
            
            if (isPlayerFirst) a = 0.25f;
            else a = -0.25f;

            firstTime = false;
            NPCText.text = NPCTalk[PlayerPrefs.GetInt("language")];
            NPCText.DOFade(1, 1f).SetDelay(0.25f + a);
            NPCText.DOFade(0, 1f).SetDelay(showWordsTime);

            PlayerText.text = PlayerTalk[PlayerPrefs.GetInt("language")];
            PlayerText.DOFade(1, 1f).SetDelay(0.25f - a);
            PlayerText.DOFade(0, 1f).SetDelay(showWordsTime + a);
        }
    }
}
