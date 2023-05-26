using TMPro;
using UnityEngine;

public class EducationText : MonoBehaviour
{
    public GameObject commentText;
    public GameObject goWhichLevelText;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;

    void Start()
    {
        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
       
        EducationLanguage();
        GoLevelText();
    }

    public void EducationLanguage()
    {
        currentLanguage = PlayerPrefs.GetInt("language");
        if (int.Parse(PlayerPrefs.GetString("nextLevelName")) < 5)
        {
            commentText.SetActive(true);
            commentText.GetComponent<TextMeshProUGUI>().text = dialog[0 * totalLanguage + currentLanguage];
        }
    }

    public void GoLevelText()
    {
        if(PlayerPrefs.GetInt("language") == 0)        //türkce
            goWhichLevelText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("nextLevelName") + dialog[1 * totalLanguage + currentLanguage];
        else if(PlayerPrefs.GetInt("language") == 1)   //english
            goWhichLevelText.GetComponent<TextMeshProUGUI>().text = dialog[1 * totalLanguage + currentLanguage] + PlayerPrefs.GetString("nextLevelName");
    }
}
