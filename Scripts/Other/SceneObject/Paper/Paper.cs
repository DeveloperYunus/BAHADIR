using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Paper : MonoBehaviour
{
    public float destroyTime;
    [HideInInspector]public string loreText;
    [HideInInspector]public float time, IEnumTime;
    GameObject lorePaper;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        lorePaper = GameObject.Find("LorePaper");
        StartCoroutine(ActiveCollider());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            int a = Random.Range(0, 2);
            if(a==0)
                audioManager.playSound("paper1");
            else
                audioManager.playSound("paper1");

            lorePaper.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = loreText;
            lorePaper.GetComponent<RectTransform>().DOScale(1, 0f);
            lorePaper.GetComponent<CanvasGroup>().DOFade(0.9f, 0.5f);

            lorePaper.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(time + 0.5f);
            lorePaper.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetDelay(time);
            Destroy(gameObject, destroyTime);
        }
    }

    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(IEnumTime);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
