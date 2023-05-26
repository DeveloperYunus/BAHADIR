using UnityEngine;
using TMPro;
using DG.Tweening;

public class ZerlikScreenTalk : MonoBehaviour
{
    public TextMeshProUGUI zerlikTalk;
    public string[] words;
    public float readTime;

    bool oneTime;

    private void Start()
    {
        oneTime = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (oneTime && collision.CompareTag("Player"))
        {
            oneTime = false;
            zerlikTalk.text = words[PlayerPrefs.GetInt("language")];
            zerlikTalk.GetComponent<TextMeshProUGUI>().DOFade(1, 1);
            zerlikTalk.GetComponent<TextMeshProUGUI>().DOFade(0, 1.5f).SetDelay(readTime);
        }
    }
}