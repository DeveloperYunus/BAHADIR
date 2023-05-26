using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class AfterCredit : MonoBehaviour
{
    TextMeshProUGUI tmp;
    Rigidbody2D rb;
    [HideInInspector] public bool up;
    [HideInInspector] public float slideSpeed;
    int a;

    [Multiline(5)]
    public string[] credits;
    public CanvasGroup image;
    public string[] thanksLanguage;

    private void Start()
    {
        CloseShow(0f);
        tmp = GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
        up = false;

        if (PlayerPrefs.GetString("nextLevelName") == "11" && PlayerPrefs.GetString("isFirstFinish") != "no")
        {
            StartShow(0);
        }
    }

    public void StartShow(float startTime)
    {
        image.GetComponent<RectTransform>().DOScale(1, 0f);
        image.DOFade(1, startTime);
        tmp.DOFade(1, 2).SetDelay(startTime);

        if (PlayerPrefs.GetString("nextLevelName") == "11" && PlayerPrefs.GetString("isFirstFinish") != "no")
        {
            PlayerPrefs.SetString("isFirstFinish", "no");
            tmp.text = "\n\n\n\n\n\n\n\n" + thanksLanguage[PlayerPrefs.GetInt("language")] + "\n\n\n" + credits[PlayerPrefs.GetInt("language")];
        }
        else
        {
            tmp.text = "\n\n\n\n\n\n\n\n\n\n" + credits[PlayerPrefs.GetInt("language")];
        }
        StartCoroutine(Credit());   
    }
    public void CloseShow(float closeTime)
    {
        if (closeTime > 1) a = 1;
        else a = 0;

        tmp.DOFade(0, 0).SetDelay(a * closeTime);
        image.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(a * (closeTime + 1));
        image.DOFade(0, a * 2).SetDelay(a * closeTime);
    }
    private void FixedUpdate()
    {
        if(up)
        {
            slideSpeed = Mathf.Lerp(slideSpeed, 43f, 0.01f);
        }
        else
        {
            slideSpeed = Mathf.Lerp(slideSpeed, 0, 0.05f);
        }
        rb.velocity = new Vector2(default, slideSpeed);
    }
    IEnumerator Credit()
    {
        yield return new WaitForSeconds(2f);
        up = true;
    }
}
