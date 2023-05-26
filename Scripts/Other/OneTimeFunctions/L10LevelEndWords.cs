using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class L10LevelEndWords : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] language;

    private void Start()
    {
        language[0] = "Bahadýr kararýný en sonunda Zerlik ile yüzleþeceði Özgürlük Savaþýndan yana vermiþtir...\nÝlk hedefi ise 4 týlsým ve 1 þiþe süt." +
                      "\n\nFakat önce hazýrlanmasý gerek.";

        language[1] = "Bahadir made his decision in favor of the War of Freedom, in which he will finally face Zerlik...\nHis first target is 4 talismans and 1 bottle of milk." +
                      "\n\nBut it has to be prepared first.";
    }

    public void LevelEnd()
    {
        StartCoroutine(Timee());
    }

    IEnumerator Timee()
    {
        yield return new WaitForSeconds(1.6f);
        text.DOFade(1, 0.001f);
        text.text = language[PlayerPrefs.GetInt("language")];
        Time.timeScale = 0.002f;        
        yield return new WaitForSeconds(0.04f);
        Time.timeScale = 1;       
    }
}
