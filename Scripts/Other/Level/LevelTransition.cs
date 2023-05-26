using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public static LevelTransition instance;
    public SaveLoad saveDataHolderForSkillPoint;

    public Animator animator;
    public TMP_Text text;
    public string[] levelMessage;


    private void Start()
    {
        instance = this;
        text.GetComponent<TextMeshProUGUI>().text = levelMessage[PlayerPrefs.GetInt("language")];
    }

    public void GoLevel(string sceneName, float scenePastAnimTime)
    {
        if(SceneManager.GetActiveScene().name == "10")
        {
            GetComponent<L10LevelEndWords>().LevelEnd();
        }
        saveDataHolderForSkillPoint.playerData.skillLevel += 2;
        saveDataHolderForSkillPoint.SaveSkill();
        StartCoroutine(GoLevelBasic(sceneName ,scenePastAnimTime));
    }
    IEnumerator GoLevelBasic(string sceneName, float animationTime)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
            animator.SetTrigger("Start");

        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(sceneName);
        
        if (GameManager.GameM.gameObject)        
            Destroy(GameManager.GameM.gameObject);        
    }
    public void GoLevelBasicForEnum(string sceneName, float animationTime)          //bunu yazdýk cunku Ienumaratorlara dýsardan eriþmeyi bilmiyoruz
    {
        StartCoroutine(GoLevelBasic(sceneName, animationTime));
    } 

    public void OtherSceneAnimPlay(string animName)
    {
        animator.Play(animName);
    }
}
