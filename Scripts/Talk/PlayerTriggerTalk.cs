using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerTriggerTalk : MonoBehaviour
{
    public GameObject playerTalk;
    public float readTime;
    public string PPrefsName;
    public string TalkExplanation;                                                   //nayin trigger i oldugunu acýklar
    public List<string> whatIThink = new List<string>(); 

    int nextThink;
    TextMeshPro playerText;

    private void Start()
    {
        PlayerPrefs.SetInt(PPrefsName, 0);
        PlayerPrefs.SetInt("konus", 1);                     //bu herhangi bir kosul gerektýrmeyen dusunceler için kullanýlacak.
        nextThink = 0;

        playerText = playerTalk.GetComponent<TextMeshPro>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (nextThink == 0 && other.CompareTag("Player") && PlayerPrefs.GetInt(PPrefsName) == 1) 
        {
            nextThink = 1;
            playerText.text = whatIThink[PlayerPrefs.GetInt("language")];
            playerText.DOFade(0f, 0.4f);
            playerText.DOFade(0.8f, 0.3f).SetDelay(0.4f);
            playerText.DOFade(0, 0.3f).SetDelay(readTime).OnComplete(DoNullText);
        }
    }

    void DoNullText()
    {
        playerText.text = null;
    }
}
