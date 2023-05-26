using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Achievement1 : MonoBehaviour
{
    public static float tenMinuteTimer;
    public SpriteRenderer sword;
    public GameObject swordBtn, beltedSwordInventory;
    public TextMeshProUGUI zerlikTalk;
    public SaveLoad saveDataHolderForSkillPoint;
    public string[] timeWinZerlikSay;

    bool areYouWinGame;
    float zaman;

    void Start()
    {
        tenMinuteTimer = 600;
        areYouWinGame = false;

        sword.DOFade(0, 0f);
        swordBtn.GetComponent<CanvasGroup>().DOFade(0, 0f);
        beltedSwordInventory.GetComponent<RectTransform>().DOScale(0, 0f);
        swordBtn.SetActive(false);
    }
    private void Update()
    {
        if (Time.time >= zaman)
        {
            zaman = Time.time + 0.5f;

            tenMinuteTimer -= Time.deltaTime;
            if (tenMinuteTimer < 0)            
                YouWinTheGame();          
        }
    }

    public void ZerlikGiveSword()
    {
        swordBtn.SetActive(true);
        swordBtn.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
        beltedSwordInventory.GetComponent<RectTransform>().DOScale(1, 0f);
        sword.DOFade(1, 0.5f);
    }


    void YouWinTheGame()
    {
        zerlikTalk.text = timeWinZerlikSay[PlayerPrefs.GetInt("language")];
        zerlikTalk.GetComponent<RectTransform>().DOScale(1, 0f);
        zerlikTalk.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

        PlayerPrefs.SetString("typeOfFinish", "tenMinute");
        PlayerPrefs.DeleteKey("ilkAcilis");
        PlayerPrefs.SetString("nextLevelName", "0");
        areYouWinGame = true;
    }

    public void YouWinBtn()//10dk dolunca ekranda bu butona týklayýnca ana menuye donecek
    {
        if(areYouWinGame)
            StartCoroutine(GoLevel("MainMenu", 2f));
    }

    public void YouAccept()
    {
        saveDataHolderForSkillPoint.playerData.skillLevel += 2;
        saveDataHolderForSkillPoint.GetComponent<SaveLoad>().SaveSkill();
        saveDataHolderForSkillPoint.GetComponent<SaveLoad>().SaveItem();
        PlayerPrefs.SetString("nextLevelName", "2");
        StartCoroutine(GoLevel("2",3f));
    }

    IEnumerator GoLevel(string sceneName,float firstCDtime)
    {
        yield return new WaitForSeconds(firstCDtime);
        GameObject.Find("LevelLoader").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
        Destroy(GameManager.GameM.gameObject);
    }
}
