using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject envanterBacground, itemInfoPanle;
    public static float stunForSlow;

    AudioManager audioManager;
    float zaman;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        GameManager.GameM.isPaused = false;
        stunForSlow = 1f;
        Time.timeScale = 1.0f;
        envanterBacground.GetComponent<RectTransform>().DOScale(0, 0f);
        envanterBacground.GetComponent<CanvasGroup>().DOFade(0, 0f);  
    }
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
            InventoryControl();
        if (GameManager.GameM.isPaused && stunForSlow == 0)
            ResumeEnvanter();
    }
    public void InventoryControl()
    {
        audioManager.playSound("button1");

        if(GameManager.GameM.isPaused && stunForSlow != 0)
            ResumeEnvanter();        
        else if (stunForSlow != 0)            
            PauseEnvanter();      
    }

    private void ResumeEnvanter()
    {
        envanterBacground.GetComponent<RectTransform>().DOScale(0, 0f);
        envanterBacground.GetComponent<CanvasGroup>().DOFade(0, 0f);
        if (itemInfoPanle != null)
        {
            itemInfoPanle.GetComponent<RectTransform>().DOScale(0f, 0f);
            itemInfoPanle.GetComponent<CanvasGroup>().DOFade(0, 0f);
        }
        Time.timeScale = 1.0f;
        GameManager.GameM.isPaused = false;
    }
    private void PauseEnvanter()
    {
        envanterBacground.GetComponent<RectTransform>().DOScale(1, 0f);
        envanterBacground.GetComponent<CanvasGroup>().DOFade(1, 0f);
        if (itemInfoPanle != null)
        {
            itemInfoPanle.GetComponent<RectTransform>().DOScale(0f, 0f);
            itemInfoPanle.GetComponent<CanvasGroup>().DOFade(0, 0f);
        }
        Time.timeScale = 0.2f;
        GameManager.GameM.isPaused = true;
    }
}
