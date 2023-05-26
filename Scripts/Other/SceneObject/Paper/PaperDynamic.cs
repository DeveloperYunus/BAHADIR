using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PaperDynamic : MonoBehaviour
{
    public float destroyTime;
    public string[] loreText;

    [HideInInspector]public float IEnumTime;
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

            int aa = loreText.Length;
            lorePaper.GetComponent<LorePaper>().stringNumber = 0;
            lorePaper.GetComponent<LorePaper>().strings = new string[aa];
            for (int i = 0; i < aa; i++)
            {
                lorePaper.GetComponent<LorePaper>().strings[i] = loreText[i];
            }
            lorePaper.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = loreText[0];
            lorePaper.GetComponent<RectTransform>().DOScale(1, 0f);
            lorePaper.GetComponent<CanvasGroup>().DOFade(0.9f, 0.5f);
            Destroy(gameObject, destroyTime);
        }
    }

    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(IEnumTime);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
