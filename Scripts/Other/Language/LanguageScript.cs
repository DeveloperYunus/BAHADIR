using UnityEngine;
using TMPro;

public class LanguageScript : MonoBehaviour
{
    public TextMeshProUGUI textt;

    public void LanguageBtn(int language)
    {
        PlayerPrefs.SetInt("language", language);

        if (PlayerPrefs.GetInt("language") == 0) 
        {
            textt.text = "Dil";
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            textt.text = "L anguage";
        }
    }
}
