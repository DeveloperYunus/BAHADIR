using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TalkNPClerDynamic : MonoBehaviour
{
    public TextMeshPro[] TMProObject;
    public Language[] talkForTexts;
    public float[] readTime;
    public float[] speakOrder;                      //konusma sýrasýný ve gecýkmeleri belirler

    bool oneTime;

    private void Start()
    {
        oneTime = true;

        for (int i = 0; i < TMProObject.Length; i++)
        {
            TMProObject[i].DOFade(0, 0);
            TMProObject[i].text = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && oneTime) 
        {
            oneTime = false;
            StartCoroutine(Talks());
        }
    }

    IEnumerator Talks()
    {
        for (int i = 0; i < talkForTexts.Length / TMProObject.Length; i++)
        {
            i *= 3;
            for (int a = 0; a < TMProObject.Length; a++)
            {
                TMProObject[a].DOFade(1, 0.7f).SetDelay(speakOrder[a + i]);
                TMProObject[a].text = talkForTexts[a + i].differentLanguage[PlayerPrefs.GetInt("language")];
                TMProObject[a].DOFade(0, 0.5f).SetDelay(readTime[i / 3] - 1);
            }

            yield return new WaitForSeconds(readTime[i / 3]);
        }

        for (int i = 0; i < TMProObject.Length; i++)
        {
            TMProObject[i].DOFade(0, 1);
        }      
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < TMProObject.Length; i++)
        {
            TMProObject[i].text = null;
        }
    }
}

[System.Serializable]
public class Language
{
    public string[] differentLanguage;
}