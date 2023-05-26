using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class NPCTalkToNPCStraight : MonoBehaviour
{
    public TextMeshPro NPCText1, NPCText2;
    public string[] NPCTalk, PlayerTalk;
    public string[] NPCTalk2, PlayerTalk2;
    public float showWordsTime, howManyTimestTalk;
    public bool isPlayerFirst;

    bool firstTime;

    private void Start()
    {
        firstTime = true;
        NPCText1.DOFade(0,0f);
        if (transform.localScale.x < 0)
        {
            Vector3 scaler = NPCText1.transform.localScale;
            scaler.x *= -1;
            NPCText1.transform.localScale = scaler;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && firstTime)
        {
            float a;
            if (isPlayerFirst) a = 0.25f;
            else a = -0.25f;

            firstTime = false;
            NPCText1.text = NPCTalk[PlayerPrefs.GetInt("language")];
            NPCText1.DOFade(1, 1f).SetDelay(0.25f + a);
            NPCText1.DOFade(0, 1f).SetDelay(showWordsTime);

            NPCText2.text = PlayerTalk[PlayerPrefs.GetInt("language")];
            NPCText2.DOFade(1, 1f).SetDelay(0.25f - a);
            NPCText2.DOFade(0, 1f).SetDelay(showWordsTime + a);

            if (howManyTimestTalk == 2) 
                StartCoroutine(SecondTalk());
        }
    }

    IEnumerator SecondTalk()
    {
        yield return new WaitForSeconds(1.5f + showWordsTime);

        float a;
        if (isPlayerFirst) a = 0.25f;
        else a = -0.25f;
        NPCText1.text = NPCTalk2[PlayerPrefs.GetInt("language")];
        NPCText1.DOFade(1, 1f).SetDelay(0.25f + a);
        NPCText1.DOFade(0, 1f).SetDelay(showWordsTime);

        NPCText2.text = PlayerTalk2[PlayerPrefs.GetInt("language")];
        NPCText2.DOFade(1, 1f).SetDelay(0.25f - a);
        NPCText2.DOFade(0, 1f).SetDelay(showWordsTime + a);
    }
}
