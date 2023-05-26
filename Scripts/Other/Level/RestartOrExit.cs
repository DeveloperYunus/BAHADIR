using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class RestartOrExit : MonoBehaviour
{
    public GameObject button, AYSImage;
    public Sprite restart, exit, OB;  
    public GameObject loseCoinAmount;

    AudioManager audioManager;
    public EnvanterButton eb;
    float goLevelID, startCoinHolder;
    Image AysImagee;
    // 0 = restart, 1 = OB, 2 = exit

    private void Start()
    {
        if(eb)
            startCoinHolder = eb.ourCoin;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AysImagee = AYSImage.GetComponent<Image>();

        loseCoinAmount.GetComponent<CanvasGroup>().DOFade(0, 0f);
        button.GetComponent<RectTransform>().DOScale(0, 0f);
        button.GetComponent<CanvasGroup>().DOFade(0, 0f);
    }
    public void Restart()
    {
        LoseCoinAmount();
        audioManager.playSound("button1");
        goLevelID = 0;
        AysImagee.sprite = restart;

        button.GetComponent<RectTransform>().DOScale(1, 0f);
        button.GetComponent<CanvasGroup>().DOFade(1, 0.1f);
        button.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.5f);
        button.GetComponent<CanvasGroup>().DOFade(0, 0.1f).SetDelay(0.4f);
    }
    public void BaseOfOperationTwo()
    {
        LoseCoinAmount();
        audioManager.playSound("button1");
        goLevelID = 1;
        AysImagee.sprite = OB;

        button.GetComponent<RectTransform>().DOScale(1, 0f);
        button.GetComponent<CanvasGroup>().DOFade(1, 0.1f);
        button.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.5f);
        button.GetComponent<CanvasGroup>().DOFade(0, 0.1f).SetDelay(0.4f);
    }
    public void Exit()
    {
        LoseCoinAmount();
        audioManager.playSound("button1");
        goLevelID = 2;
        AysImagee.sprite = exit;

        button.GetComponent<RectTransform>().DOScale(1, 0f);
        button.GetComponent<CanvasGroup>().DOFade(1, 0.1f);
        button.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.5f);
        button.GetComponent<CanvasGroup>().DOFade(0, 0.1f).SetDelay(0.4f);
    }
    public void RestartOrExitBtn()
    {
        audioManager.playSound("button1");

        eb.ourCoin = startCoinHolder;
        eb.EnvanterButtonSave();

        if (goLevelID == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (goLevelID == 1) 
        {
            SceneManager.LoadScene("BaseOfOperation");
        }
        else
        {
            Application.Quit();
        }

        Destroy(gameObject);
    }
    public void LoseCoinAmount()
    {
        loseCoinAmount.GetComponent<TextMeshProUGUI>().text = "- " + (eb.ourCoin - startCoinHolder).ToString();
        loseCoinAmount.GetComponent<CanvasGroup>().DOFade(1, 0.1f);
        loseCoinAmount.GetComponent<CanvasGroup>().DOFade(0, 0.1f).SetDelay(0.4f);
    }
}
