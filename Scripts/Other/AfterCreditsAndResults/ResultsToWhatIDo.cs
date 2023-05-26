using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ResultsToWhatIDo : MonoBehaviour
{
    public GameObject resultImage, levelImage;
    public TextMeshProUGUI text;
    public Sprite level3, level5, level6, level7;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;

    private void Start()
    {
        resultImage.GetComponent<RectTransform>().DOScale(0,0);
        resultImage.GetComponent<CanvasGroup>().DOFade(0,0);
        levelImage.GetComponent<CanvasGroup>().DOFade(0,0);
        text.DOFade(0,0f);

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            PlayerController.isTalking = true;
            StartCoroutine(ShowResults());
        }
    }

    IEnumerator ShowResults()
    {
        float a = 25;
        yield return new WaitForSeconds(0f);
        resultImage.GetComponent<RectTransform>().DOScale(1, 0f);
        resultImage.GetComponent<CanvasGroup>().DOFade(1, 1);
        levelImage.GetComponent<Image>().DOFade(0, 0f);
        text.DOFade(1, 1f).SetDelay(1f);
        text.text = dialog[0 * totalLanguage + currentLanguage];
        text.DOFade(0, 1f).SetDelay(a - 12f);

        yield return new WaitForSeconds(a-9);
        levelImage.GetComponent<Image>().sprite = level3;
        levelImage.GetComponent<Image>().DOFade(1, 1f).SetDelay(1f);
        text.DOFade(1, 1f).SetDelay(1f);
        ResultsSwicth(1);

        levelImage.GetComponent<Image>().DOFade(0, 1f).SetDelay(a - 2f);
        text.DOFade(0, 1f).SetDelay(a - 2f);

        yield return new WaitForSeconds(a);
        levelImage.GetComponent<Image>().sprite = level5;
        levelImage.GetComponent<Image>().DOFade(1, 1f).SetDelay(1f);
        text.DOFade(1, 1f).SetDelay(1f);
        ResultsSwicth(2);
        levelImage.GetComponent<Image>().DOFade(0, 1f).SetDelay(a - 2f);
        text.DOFade(0, 1f).SetDelay(a - 2f);

        yield return new WaitForSeconds(a);
        levelImage.GetComponent<Image>().sprite = level6;
        levelImage.GetComponent<Image>().DOFade(1, 1f).SetDelay(1f);
        text.DOFade(1, 1.5f).SetDelay(1f);
        ResultsSwicth(3);
        levelImage.GetComponent<Image>().DOFade(0, 1f).SetDelay(a - 2f);
        text.DOFade(0, 1f).SetDelay(a - 2f);

        yield return new WaitForSeconds(a);
        levelImage.GetComponent<Image>().sprite = level7;
        levelImage.GetComponent<Image>().DOFade(1, 1f).SetDelay(1f);
        text.DOFade(1, 1.5f).SetDelay(1f);
        ResultsSwicth(4);
        levelImage.GetComponent<Image>().DOFade(0, 1f).SetDelay(a - 11f);
        text.DOFade(0, 1f).SetDelay(a - 11f);

        yield return new WaitForSeconds(a - 9);
        levelImage.GetComponent<Image>().DOFade(0, 1f).SetDelay(1f);
        text.DOFade(1, 1.5f).SetDelay(1f);
        ResultsSwicth(5);

        yield return new WaitForSeconds(a - 9);
        levelImage.GetComponent<Image>().DOFade(0, 1f);
        text.DOFade(0, 1f);
        resultImage.GetComponent<CanvasGroup>().DOFade(1, 1f).SetDelay(1.2f);
        resultImage.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(2.2f);
        PlayerController.isTalking = false;
    }

    void ResultsSwicth(int a)
    {
        switch (a)
        {
            case 1:
                text.text = dialog[1 * totalLanguage + currentLanguage];

                if (PlayerPrefs.GetString("level3plyrkatlettimi") == "evet")
                {
                    text.text += dialog[2 * totalLanguage + currentLanguage];
                }
                else if (PlayerPrefs.GetString("kiliciVerdimi") == "verdi")
                {
                    text.text += dialog[3 * totalLanguage + currentLanguage];
                }
                else if (PlayerPrefs.GetString("level3Secim") == "yalanSoyledi")
                {
                    text.text += dialog[4 * totalLanguage + currentLanguage];
                }
                else if (PlayerPrefs.GetString("level3Secim") == "yardimEtti")
                {
                    text.text += dialog[5 * totalLanguage + currentLanguage];
                }
                else if (PlayerPrefs.GetString("level3Secim") == "Savaþýn")
                {
                    text.text += dialog[6 * totalLanguage + currentLanguage];
                }
                else//kayýtsýz
                {
                    text.text += dialog[7 * totalLanguage + currentLanguage];
                }
                break;

            case 2:
                text.text = dialog[8 * totalLanguage + currentLanguage];

                if (PlayerPrefs.GetString("MageIsDead") == "yes")
                {
                    text.text += dialog[9 * totalLanguage + currentLanguage];
                }
                else
                {
                    text.text += dialog[10 * totalLanguage + currentLanguage];
                }
                break;

            case 3:
                text.text = dialog[11 * totalLanguage + currentLanguage];

                if (PlayerPrefs.GetString("L6VillagesInPeace") == "evet")
                {
                    text.text += dialog[12 * totalLanguage + currentLanguage];
                }
                else if (PlayerPrefs.GetString("L6VillagesInPeace") == "denedi")
                {
                    text.text += dialog[13 * totalLanguage + currentLanguage];
                }
                else
                {
                    text.text += dialog[14 * totalLanguage + currentLanguage];
                }
                break;

            case 4:
                text.text = dialog[15 * totalLanguage + currentLanguage];

                if (PlayerPrefs.GetString("level7") == "col")
                {
                    text.text += dialog[16 * totalLanguage + currentLanguage];
                }
                else
                {
                    text.text += dialog[17 * totalLanguage + currentLanguage];
                }
                break;

            case 5:
                text.text = dialog[18 * totalLanguage + currentLanguage];
                break;
        }
    }
}