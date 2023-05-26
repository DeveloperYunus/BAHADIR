using UnityEngine;
using DG.Tweening;
using TMPro;

public class _2BigGolemTalk : MonoBehaviour
{
    public TextMeshPro golemText;
    bool firstTime;
    public string[] swordGiveText, fightText;


    private void Start()
    {
        firstTime = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(firstTime && other.CompareTag("Player") && EnemyAII.canIChasePlayer)
        {         
            firstTime = false;
            golemText.text = fightText[PlayerPrefs.GetInt("language")];
            golemText.DOFade(1, 0.7f);
            golemText.DOFade(0, 0.5f).SetDelay(2.5f);            
        }
        else if (firstTime && other.CompareTag("Player") && !EnemyAII.canIChasePlayer)
        {
            firstTime = false;
            golemText.text = swordGiveText[PlayerPrefs.GetInt("language")];
            golemText.DOFade(1, 0.7f);
            golemText.DOFade(0, 0.5f).SetDelay(2.5f);
        }
    }
}
