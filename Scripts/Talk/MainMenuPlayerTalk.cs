using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MainMenuPlayerTalk : MonoBehaviour
{
    public TextMeshPro bahadirTalk;
    public GameObject tellerPanel;
    int speecCount;
    bool canPassTalk;
    
    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng                 dialog[0 * totalLanguage + currentLanguage];

    private void Start()
    {
        canPassTalk = true;
        speecCount = 0;
        bahadirTalk.text = "...";
        bahadirTalk.DOFade(1, 1f);

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
        CloseTellerPanel();
    }

    public void ScreenTapp()
    {
        if (canPassTalk)
        {
            canPassTalk = false;
            StartCoroutine(PassTalk(1.1f));

            if (PlayerPrefs.GetString("typeOfFinish") == "tenMinute")// oyunu 10 dk bekleyerek bitirince olacaklar
            {
                if (speecCount == 4)
                {
                    OpenTellerPanel();
                }

                switch (speecCount)
                {
                    case 0:
                        bahadirTalk.DOFade(0, 0f);
                        bahadirTalk.DOFade(1, 0.4f).SetDelay(0.2f);
                        bahadirTalk.text = dialog[0 * totalLanguage + currentLanguage];
                        speecCount = 1;
                        break;

                    case 1:
                        bahadirTalk.DOFade(0, 0f);
                        bahadirTalk.DOFade(1, 0.4f).SetDelay(0.2f);
                        bahadirTalk.text = dialog[1 * totalLanguage + currentLanguage];
                        speecCount = 2;
                        break;

                    case 2:
                        bahadirTalk.DOFade(0, 0f);
                        bahadirTalk.DOFade(1, 0.4f).SetDelay(0.2f);
                        bahadirTalk.text = dialog[2 * totalLanguage + currentLanguage];
                        speecCount = 3;
                        break;

                    case 3:
                        bahadirTalk.DOFade(0, 0f);
                        bahadirTalk.DOFade(1, 0.4f).SetDelay(0.2f);
                        bahadirTalk.text = dialog[3 * totalLanguage + currentLanguage];
                        speecCount = 4;
                        break;

                    case 4:
                        bahadirTalk.DOFade(0, 0f);
                        bahadirTalk.DOFade(1, 0.4f).SetDelay(0.2f);
                        bahadirTalk.text = null;
                        break;
                }
            }
        }
    }

    public void OpenTellerPanel()
    {
        tellerPanel.GetComponent<RectTransform>().DOScale(1, 0f);
        tellerPanel.GetComponent<CanvasGroup>().DOFade(1, 1f).SetDelay(0.5f);

        if (PlayerPrefs.GetString("typeOfFinish") == "tenMinute")// oyunu 10 dk bekleyerek bitirince olacaklar
        {
            tellerPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialog[4 * totalLanguage + currentLanguage];
        }
    }

    public void CloseTellerPanel()
    {
        tellerPanel.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(1f);
        tellerPanel.GetComponent<CanvasGroup>().DOFade(0, 1f);
    }

    IEnumerator PassTalk(float time)
    {
        yield return new WaitForSeconds(time);
        canPassTalk = true;
    }
}
