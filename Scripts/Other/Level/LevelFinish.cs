using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public static bool levelFinished;

    public EnvanterButton eb;
    public string nextLevelName;
    bool firstTime;


    private void Start()
    {
        levelFinished = false;
        firstTime = true;
        if(SceneManager.GetActiveScene().name != "MainMenu")
            PlayerPrefs.SetString("nextLevelName", (int.Parse(nextLevelName) - 1).ToString());                      //sorunlu KISIM BUUUUUUUUUUUU 13.02.2022
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && firstTime)
        {
            eb.EnvanterButtonSave();
            levelFinished = true;
            PlayerPrefs.SetString("nextLevelName",nextLevelName);
            LevelTransition.instance.GoLevel("BaseOfOperation", 3f);
        }
    }
}
