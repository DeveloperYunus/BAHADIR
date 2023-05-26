using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class OBSetting : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject languageText, soundText, resetText;
    public int howMuchLanguage;
    public string[] stringFordifferentLanguage;
    public GameObject areYouSureReset, areYouRealySureReset;
    public Slider soundSlider;

    bool isSettingPanelOpen;
    float resetBtnTimer;
    AudioManager audioManager;
    Skill dataHolder;

    [Header("TMP")]
    public TextMeshProUGUI versionText;
    public TextMeshProUGUI quitText, asd;
    public EducationText et;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        soundSlider.value = PlayerPrefs.GetInt("soundSliderAmount", 5);
        dataHolder = GetComponent<SaveLoad>().playerData;
        isSettingPanelOpen = false;
 
        settingPanel.GetComponent<RectTransform>().DOScale(0, 0f);
        settingPanel.GetComponent<CanvasGroup>().DOFade(0, 0f);
        areYouSureReset.GetComponent<RectTransform>().DOScale(0, 0);
        areYouSureReset.GetComponent<RectTransform>().DOScale(0, 0);
        areYouRealySureReset.GetComponent<RectTransform>().DOScale(0, 0);

        quitText.text = stringFordifferentLanguage[PlayerPrefs.GetInt("language") + howMuchLanguage * 3];
        versionText.text = "Version  " + Application.version + "   " + PlayerPrefs.GetString("nextLevelName");
    }
    private void Update()
    {
        if (resetBtnTimer >= 0) 
        {
            resetBtnTimer -= Time.deltaTime;
            if (resetBtnTimer < 0)
            {
                areYouSureReset.GetComponent<RectTransform>().DOScale(0, 0.2f).SetEase(Ease.InBack);
                areYouRealySureReset.GetComponent<RectTransform>().DOScale(0, 0.2f).SetEase(Ease.InBack);
            }
        }      
    }

    public void SettingBtn()
    {
        audioManager.playSound("button1");
        if (isSettingPanelOpen)
        {
            isSettingPanelOpen = false;
            settingPanel.GetComponent<RectTransform>().DOScale(0.9f, 0.4f);
            settingPanel.GetComponent<CanvasGroup>().DOFade(0, 0.4f);
            settingPanel.GetComponent<RectTransform>().DOScale(0f, 0f).SetDelay(0.4f);

        }
        else
        {
            isSettingPanelOpen = true;
            settingPanel.GetComponent<RectTransform>().DOScale(0.9f, 0f);
            settingPanel.GetComponent<RectTransform>().DOScale(1, 0.4f);
            settingPanel.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
        }

        languageText.GetComponent<TextMeshProUGUI>().text = stringFordifferentLanguage[PlayerPrefs.GetInt("language")];
        soundText.GetComponent<TextMeshProUGUI>().text = stringFordifferentLanguage[PlayerPrefs.GetInt("language") + howMuchLanguage];
        resetText.GetComponent<TextMeshProUGUI>().text = stringFordifferentLanguage[PlayerPrefs.GetInt("language") + howMuchLanguage * 2];
    }
    public void LanguagegBtn(int dilPPrefsi)
    {
        audioManager.playSound("button6");
        GetComponent<RectTransform>().DOScale(1f, 0.1f).SetEase(Ease.InBack).SetDelay(0.1f);
        PlayerPrefs.SetInt("language", dilPPrefsi);
        languageText.GetComponent<TextMeshProUGUI>().text = stringFordifferentLanguage[PlayerPrefs.GetInt("language")];
        soundText.GetComponent<TextMeshProUGUI>().text = stringFordifferentLanguage[PlayerPrefs.GetInt("language") + howMuchLanguage];
        resetText.GetComponent<TextMeshProUGUI>().text = stringFordifferentLanguage[PlayerPrefs.GetInt("language") + howMuchLanguage * 2];
        quitText.text = stringFordifferentLanguage[PlayerPrefs.GetInt("language") + howMuchLanguage * 3];
        
        et.EducationLanguage();
        et.GoLevelText();
    }

    public void ResetFirstBtn()
    {
        audioManager.playSound("button6");
        resetBtnTimer = 3f;
        areYouSureReset.GetComponent<RectTransform>().DOScale(1, 0.2f).SetEase(Ease.OutBack);
    }
    public void ResetSecondtBtn()
    {
        audioManager.playSound("button6");
        resetBtnTimer = 3f;
        areYouRealySureReset.GetComponent<RectTransform>().DOScale(1, 0.2f).SetEase(Ease.OutBack);
    }
    public void AbsolutlyResetBtn()
    {
        audioManager.playSound("button6");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("resetGame", 1);
        //PlayerPrefs.DeleteKey("ilkAcilis");                                                   bunu delete all fonksýyonunu koyduk diye kapattýk
        PlayerPrefs.SetString("nextLevelName", "0");
        areYouSureReset.GetComponent<RectTransform>().DOScale(0, 0.2f).SetEase(Ease.InBack);
        areYouRealySureReset.GetComponent<RectTransform>().DOScale(0, 0.2f).SetEase(Ease.InBack);

        dataHolder.can = 0;
        dataHolder.enerji = 0;
        dataHolder.zýrh = 0;
        dataHolder.buyuHasari = 0;
        dataHolder.fizikselHasar = 0;
        dataHolder.canCalma = 0;
        dataHolder.skillLevel = 0;

        dataHolder.canYenilenmesi = 0;
        dataHolder.canYenilenmesiPasif = 0;
        dataHolder.enerjiYenilenmesi = 0;
        dataHolder.enerjiYenilenmesiPasif = 0;

        GetComponent<SaveLoad>().SaveSkill();
        GetComponent<SaveLoad>().SaveResetItem();

        GameManager.GameM.GoManinMenu();
    }
    public void SoundSettingSlider()
    {
        audioManager.playSound("button6");
        PlayerPrefs.SetInt("soundSliderAmount", (int)soundSlider.value);
        audioManager.SetSoundPP();
    }
    public void QuitBtn()
    {
        audioManager.playSound("button1");
        Application.Quit();
    }
}
